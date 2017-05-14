using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace HospitalCeo.Building
{
    public class PlacingComponent : RenderableComponent, IUpdatable
    {
        public override float width
        {
            get
            {
                return 10000;
            }
        }

        public override float height
        {
            get
            {
                return 10000;
            }
        }

        private bool isActive = false;
        private bool carryOnAfterSuccesfullPlace = true;
        private Tile lastTileUpdated;
        private Vector2 tileSize;
        private Vector2 position;
        private Action OnPlacementCancelled;
        private Action OnPlacementSuccessfull;
        private Action<Tile> OnPlacementMoved;
        private Action OnPlacementCompleted;
        private bool shouldClearActionsAfterPlacement = false;
        private bool refractoryPeriod = false;

        public PlacingComponent()
        {
            tileSize = Vector2.One;
        }

        public override void render(Graphics graphics, Camera camera)
        {
            if (!isActive)
                return;

            graphics.batcher.drawRect(new Rectangle((int)position.X - 50, (int)position.Y - 50, (int)tileSize.X * 100, (int)tileSize.Y * 100), new Color(Color.Green, 0.2f));
        }

        void IUpdatable.update()
        {
            if (!isActive)
                return;

            // Get current tile
            Tile currentTile = WorldController.GetMouseOverTile();
            if (currentTile == null)
                return;

            // Left and Right mb checks
            if (InputManager.IsLeftMouseDown() && !refractoryPeriod)
            {
                PlacementSuccessfull();
                return;
            }

            if (InputManager.IsRightMouseDown() && !refractoryPeriod)
            {
                PlacementCancelled();
                return;
            }

            if (!InputManager.IsLeftMouseDown() && refractoryPeriod)
            {
                refractoryPeriod = false;
            }

            // Check if we should move the tile
            if (lastTileUpdated == currentTile)
                return;

            lastTileUpdated = currentTile;
            position = currentTile.GetPosition();
            PlacementMoved();
        }

        public void StartPlacing(Vector2 tileSize, bool carryOnAfterSuccesfullPlace, bool shouldClearActionsAfterPlacement)
        {
            this.tileSize = tileSize;
            isActive = true;
            this.carryOnAfterSuccesfullPlace = carryOnAfterSuccesfullPlace;
            this.shouldClearActionsAfterPlacement = shouldClearActionsAfterPlacement;
        }

        public void RegisterOnPlacementCancelled(Action action)
        {
            OnPlacementCancelled += action;
        }

        public void RegisterOnPlacementSuccessfull(Action action)
        {
            OnPlacementSuccessfull += action;
        }

        public void RegisterOnPlacementMoved(Action<Tile> action)
        {
            OnPlacementMoved += action;
        }

        public void RegisterOnPlacementCompleted(Action action)
        {
            OnPlacementCompleted += action;
        }

        private void PlacementCancelled()
        {
            carryOnAfterSuccesfullPlace = false;
            OnPlacementCancelled?.Invoke();
            PlacementCompleted();
        }

        private void PlacementSuccessfull()
        {
            OnPlacementSuccessfull?.Invoke();
            PlacementCompleted();
        }

        private void PlacementMoved()
        {
            OnPlacementMoved?.Invoke(lastTileUpdated);
        }

        private void PlacementCompleted()
        {
            OnPlacementCompleted?.Invoke();

            if (carryOnAfterSuccesfullPlace)
            {
                refractoryPeriod = true;
                return;
            }

            isActive = false;
            position = Vector2.One;
            tileSize = Vector2.One;
            lastTileUpdated = null;

            if (shouldClearActionsAfterPlacement)
            {
                OnPlacementCancelled = null;
                OnPlacementSuccessfull = null;
                OnPlacementMoved = null;
                OnPlacementCompleted = null;
            }
        }

        public void ShouldCarryOnAfterPlaced(bool b)
        {
            this.carryOnAfterSuccesfullPlace = b;
        }

        public bool IsCarryingOnAfterPlaced()
        {
            return this.carryOnAfterSuccesfullPlace;
        }

        public bool IsPlacing()
        {
            return isActive;
        }
    }
}
