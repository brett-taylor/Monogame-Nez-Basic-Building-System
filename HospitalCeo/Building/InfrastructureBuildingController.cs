using Nez;
using Nez.Console;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HospitalCeo.World;
using HospitalCeo.Components;

/*
 * Brett Taylor
 * Used in building of resizable buildings, called infrastructure items.
 * These are e.g. roads, fencing, walls, flooring.
 */

namespace HospitalCeo.Building
{
    public static class InfrastructureBuildingController
    {
        private static Texture2D baseSprite;
        private static Entity prototypeBuilding;
        private static bool isLeftMouseButtonDown = false;
        private static bool rightClickBuildRefractoryTime = false; // This is used for when the player cancels a resizing - they must first take their finger off the lmb to start resizing
        private static Vector2 originalStartingTile, startingTile, lastUpdateTile, buildTile;
        //private static BuildingRuler buildingRulerWidth;
        //private static BuildingRuler buildingRulerHeight;
        private static Vector2 sizeOfBuilding;
        private static System.Type buildingType;
        private static bool oneSquareWide = false;

        public static void Initialise()
        {
            baseSprite = WorldController.SCENE.content.Load<Texture2D>("hospitalceo/ClearSprite");
        }

        public static void StartBuilding(System.Type type)
        {
            DebugConsole.instance.log("Started Building " + type);

            // Check we are building already
            if (prototypeBuilding != null)
            {
                DebugConsole.instance.log("Already Building");
                return;
            }

            buildingType = type;

            // Creating the prototype gameobject
            prototypeBuilding = WorldController.SCENE.createEntity("Prototype building");
            prototypeBuilding.addComponent<Sprite>(new Sprite(baseSprite));
            prototypeBuilding.transform.position = WorldController.GetMouseOverTile().position;

            // Set up some variables used in the logic
            originalStartingTile = prototypeBuilding.transform.position;
            startingTile = prototypeBuilding.transform.position;
            lastUpdateTile = new Vector2(-1, -1);
            sizeOfBuilding = new Vector2(1, 1);
            buildTile = new Vector2(-1, -1);
            isLeftMouseButtonDown = false;

            // Create the building rulers
            //buildingRulerWidth = new BuildingRuler(false);
            //buildingRulerHeight = new BuildingRuler(true);

            // Grab if it should be one square wide or not.
            Building tempBuilding = (Building)System.Activator.CreateInstance(type, new Vector2(-1000, -1000));
            oneSquareWide = tempBuilding.OneSquareWidth();
            tempBuilding.Destory();
            tempBuilding = null;
        }

        public static void Update()
        {
            // Checks
            if (prototypeBuilding == null) return;
            if (WorldController.GetMouseOverTile() == null)
            {
                DebugConsole.instance.log("Not on a valid tile");
                return;
            }

            // If the left mouse button is down now but was not last update, preb to start resizing the building
            if (Controls.IsLeftMouseDown() & !isLeftMouseButtonDown & !rightClickBuildRefractoryTime)
            {
                isLeftMouseButtonDown = true;
                startingTile = WorldController.GetMouseOverTile().position;
                lastUpdateTile = new Vector2(-1, -1);
            }

            // Left mouse button is down and was down last update. We are resizing the building currently
            if (Controls.IsLeftMouseDown() & isLeftMouseButtonDown)
                DraggingUpdate();

            if ((!Controls.IsLeftMouseDown() || rightClickBuildRefractoryTime) & !isLeftMouseButtonDown)
                NotDraggingUpdate();

            // If right click is pressed and we are currently not resizing - cancel the building all together
            if (Controls.IsRightMouseDown() & !isLeftMouseButtonDown & !rightClickBuildRefractoryTime)
            {
                StopBuilding();
            }

            // If right click is pressed and we are currently resizing - Cancel the resizing, set the building back to 1x1
            if (Controls.IsRightMouseDown() & isLeftMouseButtonDown)
            {
                isLeftMouseButtonDown = false;
                rightClickBuildRefractoryTime = true;
                prototypeBuilding.transform.localScale = new Vector2(1, 1);
                sizeOfBuilding = new Vector2(1, 1);
                //buildingRulerWidth.Hide();
                //buildingRulerHeight.Hide();
            }

            // If left click is up and last frame we were building
            if (!Controls.IsLeftMouseDown() & isLeftMouseButtonDown)
            {
                SuccessfullBuilding();
                StopBuilding();
            }

            // If left click is now up and right click build refactory time is true then reset that
            if (!Controls.IsLeftMouseDown() & rightClickBuildRefractoryTime)
            {
                rightClickBuildRefractoryTime = false;
            }

            // If esc is pressed
            if (Controls.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                StopBuilding();
            }
        }

        // Used for when the left mouse button is down and the user is currently dragging. Resizing instead of repositioning.
        public static void DraggingUpdate()
        {
            // Make sure we dont call this if the mouse is still on the same tile
            if (lastUpdateTile == WorldController.GetMouseOverTile().position) return;

            // Lets flip the starting and ending positions if they will go into negatives.
            Vector2 currentTile = WorldController.GetMouseOverTile().position;
            int startingPositionX = Mathf.floorToInt(startingTile.X );
            int startingPositionY = Mathf.floorToInt(startingTile.Y);
            int endingPositionX = Mathf.floorToInt(currentTile.X);
            int endingPositionY = Mathf.floorToInt(currentTile.Y);

            if (endingPositionX < startingPositionX)
            {
                int temp = endingPositionX;
                endingPositionX = startingPositionX;
                startingPositionX = temp;
            }

            if (endingPositionY < startingPositionY)
            {
                int temp = endingPositionY;
                endingPositionY = startingPositionY;
                startingPositionY = temp;
            }

            // Work out size of building
            sizeOfBuilding = new Vector2((endingPositionX - startingPositionX) + 1, (endingPositionY - startingPositionY) + 1);

            // If the building is one square wide.
            if (oneSquareWide)
            {
                // If x is longer than y - make y one otherwise make x one
                if (sizeOfBuilding.X >= sizeOfBuilding.Y) // Horizontal
                {
                    int yOffset = (int)sizeOfBuilding.Y - 1;
                    sizeOfBuilding = new Vector2(sizeOfBuilding.X, 1);
                    buildTile = new Vector2(startingPositionX, originalStartingTile.Y);
                }
                else // Vertical
                {
                    int xOffset = (int)sizeOfBuilding.X - 1;
                    sizeOfBuilding = new Vector2(1, sizeOfBuilding.Y);
                }
            }
            else buildTile = new Vector2(startingPositionX, startingPositionY);

            // Scale accordingly
            float newXScale = sizeOfBuilding.X == 1 ? 1 : sizeOfBuilding.X / 100;
            float newYScale = sizeOfBuilding.Y == 1 ? 1 : sizeOfBuilding.Y / 100;
            prototypeBuilding.transform.localScale = new Vector2(newXScale, newYScale);

            // We must substract one from the size (as the default size of the shape is 1) and then half it to have the gameobject in the correct position.
            // This is due to unity anchoring it in the middle of the gameObject rather than the bottom-left.
            prototypeBuilding.transform.position = new Vector2(
                startingPositionX + ((sizeOfBuilding.X - 1) / 2),
                startingPositionY + ((sizeOfBuilding.Y - 1) / 2)
                );

            // Update the rulers
            //buildingRulerWidth.Update(new Vector2(startingPositionX, startingPositionY), sizeOfBuilding);
            //buildingRulerHeight.Update(new Vector2(startingPositionX, startingPositionY), sizeOfBuilding);

            // Update what tile we last hovered over
            lastUpdateTile = WorldController.GetMouseOverTile().position;
        }

        // Used for when the left mouse button is not down and instead repositioning the object instead of resizing.
        private static void NotDraggingUpdate()
        {
            prototypeBuilding.transform.position = WorldController.GetMouseOverTile().position;
        }

        public static void StopBuilding()
        {
            //buildingRulerWidth.Destory();
            //buildingRulerHeight.Destory();
            prototypeBuilding.destroy();
            prototypeBuilding = null;
        }

        public static void SuccessfullBuilding()
        {
            for (int x = 0; x < sizeOfBuilding.X; x++)
            {
                for (int y = 0; y < sizeOfBuilding.Y; y++)
                {
                    DebugConsole.instance.log("Building at : " + buildTile + " " + x + " " + y);
                    //Building newBuilding = (Building) System.Activator.CreateInstance(buildingType, new Vector2(buildTile.X + x, buildTile.Y + y));
                    //Tile t = WorldController.GetTileAt((int) buildTile.X + x, (int) buildTile.Y + y);
                    //newBuilding.OnBuildingCompleted();
                }
            }
        }
    }
}
