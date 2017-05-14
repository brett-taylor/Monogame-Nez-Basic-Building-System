using HospitalCeo.UI.World;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Console;
using System.Collections.Generic;

/*
 * Brett Taylor
 * Dragging component
 * Used in building of infrastructure and placing of zones.
 * Requires two methods:
 *  OnSuccessfullDrag(tileNumber, size) &
 *  OnStoppedDragging()
 */

namespace HospitalCeo.Building
{
    public class DraggingComponent : RenderableComponent, IUpdatable
    {
        private Color drawingColor;
        private Vector2 draggingPositionPixel;
        private Vector2 draggingSizePixel;
        private Vector2 draggingPositionTile;
        private Vector2 draggingSizeTile;
        private Tile startingTile;
        private Tile lastTileUpdated;
        private Entity buildingRuler;
        private BuildingRulerLineComponent rulerLineComponent;
        private BuildingRulerTextComponent rulerTextComponent;
        private bool isActive = false;
        private bool isLeftMouseButtonDown = false;
        private bool rightClickBuildRefractoryTime = false;
        private bool carryOnAfterSuccessfullDraw = true;
        private bool shouldShowRulerLines = true;
        private bool shouldShowRulerLengthText = true;

        public virtual void OnSuccessfullDrag(Vector2 tileNumber, Vector2 size, List<Tile> tilesAffected) { }
        public virtual void OnFinishedDragging() { }
        public override float width { get { return 10000; } } // Have to be called as part of RenderableComponent
        public override float height { get { return 10000; } } // Have to be called as part of RenderableComponent

        public DraggingComponent(Color drawingColor, bool shouldShowRulerLines, bool shouldShowRulerLengthText) : base()
        {
            this.drawingColor = drawingColor;
            this.shouldShowRulerLines = shouldShowRulerLines;
            this.shouldShowRulerLengthText = shouldShowRulerLengthText;

            // Create the building rulers;
            rulerLineComponent = new BuildingRulerLineComponent();
            rulerTextComponent = new BuildingRulerTextComponent();
            buildingRuler = WorldController.SCENE.createEntity("Building Ruler");
            buildingRuler.addComponent<BuildingRulerLineComponent>(rulerLineComponent);
            buildingRuler.addComponent<BuildingRulerTextComponent>(rulerTextComponent);
            rulerLineComponent.setEnabled(false);
            rulerTextComponent.setEnabled(false);
        }

        public override void render(Graphics graphics, Camera camera)
        {
            if (!isActive) return;
            graphics.batcher.drawRect(new Rectangle((int) draggingPositionPixel.X - 50, (int) draggingPositionPixel.Y - 50, (int) draggingSizePixel.X, (int) draggingSizePixel.Y), drawingColor);
        }

        protected void StartDragging(bool carryOnAfterSuccessfullDraw)
        {
            isActive = true;
            this.carryOnAfterSuccessfullDraw = carryOnAfterSuccessfullDraw;
        }

        void IUpdatable.update()
        {
            // Checks
            if (!isActive) return;
            if (WorldController.GetMouseOverTile() == null)
            {
                DebugConsole.instance.log("Not on a valid tile");
                return;
            }

            // Get current tile
            Tile currentTile = WorldController.GetMouseOverTile();

            // If the left mouse button is down now but was not last update, preb to star resizing the dragging zone
            if (InputManager.IsLeftMouseDown() & !isLeftMouseButtonDown & !rightClickBuildRefractoryTime)
            {
                isLeftMouseButtonDown = true;
                startingTile = WorldController.GetMouseOverTile();
                lastTileUpdated = null;
            }

            // Left mouse button is down and was down last update. We are dragging the zone currently
            if (InputManager.IsLeftMouseDown() & isLeftMouseButtonDown)
            {
                DraggingUpdate(currentTile);
            }

            // If LMB is up and 
            if ((!InputManager.IsLeftMouseDown() || rightClickBuildRefractoryTime) & !isLeftMouseButtonDown)
            {
                NotDraggingUpdate(currentTile);
            }

            // If right click is pressed and we are currently not resizing - cancel the zone all together
            if (InputManager.IsRightMouseDown() & !isLeftMouseButtonDown & !rightClickBuildRefractoryTime)
            {
                FinishDragging();
            }

            // If right click is pressed and we are currently resizing - Cancel the resizing, set the zone back to 1x1
            if (InputManager.IsRightMouseDown() & isLeftMouseButtonDown)
            {
                StopDragging();
            }

            // If left click is up and last frame we were dragging
            if (!InputManager.IsLeftMouseDown() & isLeftMouseButtonDown)
            {
                SuccesfullDrag();
            }

            // If left click is now up and right click build refactory time is true then reset that
            if (!InputManager.IsLeftMouseDown() & rightClickBuildRefractoryTime)
            {
                rightClickBuildRefractoryTime = false;
            }

            // If esc is pressed
            if (InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                FinishDragging();
            }

            // Set the current last tile updated
            lastTileUpdated = currentTile;
        }

        // Called when we are dragging
        private void DraggingUpdate(Tile tileUnderMouse)
        {
            // Check if we are still on the same tile as last frame
            if (tileUnderMouse == lastTileUpdated) return;

            // Lets flip the starting and ending positions if they will go into negatives.
            Vector2 currentTile = WorldController.GetMouseOverTile().GetTileNumber();
            int startingTileX = Mathf.floorToInt(startingTile.GetTileNumber().X);
            int startingTileY = Mathf.floorToInt(startingTile.GetTileNumber().Y);
            int endingTileX = Mathf.floorToInt(tileUnderMouse.GetTileNumber().X);
            int endingTileY = Mathf.floorToInt(tileUnderMouse.GetTileNumber().Y);

            if (endingTileX < startingTileX)
            {
                int temp = endingTileX;
                endingTileX = startingTileX;
                startingTileX = temp;
            }

            if (endingTileY < startingTileY)
            {
                int temp = endingTileY;
                endingTileY = startingTileY;
                startingTileY = temp;
            }

            // Work out size of the zone on pixels to draw
            Tile t = WorldController.GetTileAt(startingTileX, startingTileY);
            if (t == null) StopDragging();
            draggingSizePixel = WorkOutSize(new Vector2((endingTileX - startingTileX + 1), (endingTileY - startingTileY + 1)));
            draggingPositionPixel = t.GetPosition();

            // Work out the size of the zone on tiles
            draggingPositionTile = t.GetTileNumber();
            draggingSizeTile = new Vector2((endingTileX - startingTileX + 1), (endingTileY - startingTileY + 1));

            // Update the building rulers line
            rulerLineComponent.setEnabled(true);
            rulerLineComponent.update(draggingPositionPixel, draggingSizePixel);

            // Update the building rulers text
            rulerTextComponent.setEnabled(true);
            rulerTextComponent.update(draggingPositionPixel, draggingSizePixel);

            // Set the current last tile updated
            lastTileUpdated = tileUnderMouse;
        }

        // Called when we are not dragging
        private void NotDraggingUpdate(Tile tileUnderMouse)
        {
            rulerLineComponent.setEnabled(false);
            rulerTextComponent.setEnabled(false);

            // Set the position & size in pixels
            draggingPositionPixel = tileUnderMouse.GetPosition();
            draggingSizePixel = WorkOutSize(new Vector2(1, 1));

            // Set the position & size in tiles
            draggingPositionTile = tileUnderMouse.GetTileNumber();
            draggingSizeTile = new Vector2(1, 1);
        }

        // Gets rid of all the dragging stuff.
        private void StopDragging()
        {
            lastTileUpdated = null;
            startingTile = null;
            isLeftMouseButtonDown = false;
            rightClickBuildRefractoryTime = true;
            draggingSizePixel = new Vector2(0, 0);
            draggingSizeTile = new Vector2(0, 0);
            rulerLineComponent.setEnabled(false);
            rulerTextComponent.setEnabled(false);
        }

        // Used when we are fully finished with dragging - removes everything
        protected void FinishDragging()
        {
            isActive = false;
            StopDragging();
            OnFinishedDragging();
        }

        // Lets stop dragging and send the position and size to the OnSuccessfullDrag method.
        private void SuccesfullDrag()
        {
            List<Tile> tilesAffected = new List<Tile>();

            for (int x = 0; x < draggingSizeTile.X; x++)
            {
                for (int y = 0; y < draggingSizeTile.Y; y++)
                {
                    Tile t = WorldController.GetTileAt((int)draggingPositionTile.X + x, (int)draggingPositionTile.Y + y);
                    if (t == null) continue;
                    tilesAffected.Add(t);
                }
            }

            OnSuccessfullDrag(draggingPositionTile, draggingSizeTile, tilesAffected);

            // If we should carry on dragging a zone after placing lets stop dragging
            if (carryOnAfterSuccessfullDraw)
                StopDragging();
            else
                FinishDragging();
        }

        public bool IsDragging()
        {
            return isActive;
        }

        private Vector2 WorkOutSize(Vector2 size)
        {
            return new Vector2(size.X * 100, size.Y * 100);
        }
    }
}
