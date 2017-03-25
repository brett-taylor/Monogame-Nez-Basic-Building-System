using Nez;
using HospitalCeo.World;
using System;
using Microsoft.Xna.Framework;

namespace HospitalCeo.AI
{
    /*
     * Brett Taylor
     * Pathfinding component added to entities
     * Gives the component a destination tile
     * 
     */
       
    public class PathfindingComponent : Component, IUpdatable
    {
        private Action onReached;
        private Tile destinationTile;

        public PathfindingComponent() : base()
        {
        }

        public void RegisterOnReachHandle(Action action)
        {
            onReached += action;
        }

        public void DeregisterOnReachHandle(Action action)
        {
            onReached -= action;
        }

        public void ClearOnReachHandles()
        {
            if (onReached.GetInvocationList().Length == 0) return;
            foreach (Delegate action in onReached.GetInvocationList())
                onReached -= (Action) action;
        }

        public int AmountOfHandlers()
        {
            return onReached.GetInvocationList().Length;
        }

        public void SetDestinationTile(Tile t)
        {
            destinationTile = t;
        }

        void IUpdatable.update()
        {
            if (destinationTile != null)
            {
                Vector2 position = destinationTile.GetPosition() - entity.position;
                entity.position += Vector2.Normalize(position) * 2;

                Tile currentTile = WorldController.GetTileAt(entity.position);
                if (currentTile == destinationTile)
                {
                    onReached?.Invoke();
                    destinationTile = null;
                    ClearOnReachHandles();
                }
            }
        }
    }
}
