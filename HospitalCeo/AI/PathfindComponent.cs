using Nez;
using HospitalCeo.World;
using System;
using Microsoft.Xna.Framework;
using HospitalCeo.Pathfinding;

/*
* Brett Taylor
* Pathfinding component added to entities
* Gives the component a destination tile
*/

namespace HospitalCeo.AI
{
    public class PathfindComponent : Component, IUpdatable
    {
        private Mob mob;
        private Vector2 destination = new Vector2(-1, -1);
        private Vector2 nextPosition = new Vector2(-1, -1);
        private Tile destinationTile;
        private Tile nextTile;
        private Action onReached;
        private Action onFailed;
        private PathfindingAStar pathfind;
        private float posLerpTime;
        private float tileLerpTime;

        public void SetDestination(Mob mob, Vector2 destination)
        {
            if (WorldController.PATHFINDING_HUMAN_GRID == null)
                WorldController.PATHFINDING_HUMAN_GRID = new PathfindingHuman();

            Tile destinationTile = WorldController.GetTileAt((int) destination.X / 100, (int) destination.Y / 100);
            if (destinationTile == null)
            {
                Nez.Console.DebugConsole.instance.log("PathfindComponent -> SetDestinationTile -> Unable to find tile at destination. (" + destination + ")");
                Nez.Console.DebugConsole.instance.Open();
                return;
            }

            Tile currentTile = WorldController.GetTileAt((int) entity.position.X / 100, (int) entity.position.Y / 100);
            if (currentTile == null)
            {
                Nez.Console.DebugConsole.instance.log("PathfindComponent -> SetDestinationTile -> Unable to find tile under the entity's feet. (" + entity.position + ")");
                Nez.Console.DebugConsole.instance.Open();
                return;
            }

            if (!currentTile.CanPathfindTo())
            {
                bool foundTile = false;
                int i = 1;
                while (foundTile == false && i < 5)
                {
                    Tile northTile = WorldController.GetTileAt(currentTile.GetTileNumber() + new Vector2(0, -i), isTileCoords: true);
                    if (northTile != null && northTile.CanPathfindTo())
                    {
                        foundTile = true;
                        currentTile = northTile;
                        break;
                    }

                    Tile southTile = WorldController.GetTileAt(currentTile.GetTileNumber() + new Vector2(0, i), isTileCoords: true);
                    if (southTile != null && southTile.CanPathfindTo())
                    {
                        foundTile = true;
                        currentTile = southTile;
                        break;
                    }

                    Tile westTile = WorldController.GetTileAt(currentTile.GetTileNumber() + new Vector2(-i, 0), isTileCoords: true);
                    if (westTile != null && westTile.CanPathfindTo())
                    {
                        foundTile = true;
                        currentTile = westTile;
                        break;
                    }

                    Tile eastTile = WorldController.GetTileAt(currentTile.GetTileNumber() + new Vector2(i, 0), isTileCoords: true);
                    if (eastTile != null && eastTile.CanPathfindTo())
                    {
                        foundTile = true;
                        currentTile = eastTile;
                        break;
                    }

                    i++;
                }

                if (foundTile == false)
                {
                    Nez.Console.DebugConsole.instance.log("");
                    Nez.Console.DebugConsole.instance.log("PathfindComponent -> SetDestinationTile -> Unable to pathfind to the tile under the entitys feet. (" + entity.position + ")");
                    Nez.Console.DebugConsole.instance.log("PathfindComponent -> SetDestinationTile -> Could not find a tile in a 5 block radius that could be pathfinded to");
                    Nez.Console.DebugConsole.instance.log("PathfindComponent -> SetDestinationTile -> Entity should probably teleport to a always-safe position now.");
                    Nez.Console.DebugConsole.instance.log("");
                    Nez.Console.DebugConsole.instance.Open();
                    return;
                }
            }

            this.mob = mob;
            this.destination = destination;
            this.destinationTile = destinationTile;
            pathfind = new PathfindingAStar(currentTile, destinationTile);
        }

        public void RegisterOnReachHandle(Action action)
        {
            onReached += action;
        }

        public void RegisterOnFailedHandle(Action action)
        {
            onFailed += action;
        }

        public void ClearHandles()
        {
            ClearOnFailedHandles();
            ClearOnReachedHandles();
        }

        private void ClearOnReachedHandles()
        {
            if (onReached == null) return;
            if (onReached.GetInvocationList() == null) return;
            if (onReached.GetInvocationList().Length == 0) return;

            foreach (Delegate action in onReached.GetInvocationList())
                onReached -= (Action)action;
        }

        private void ClearOnFailedHandles()
        {
            if (onFailed == null) return;
            if (onFailed.GetInvocationList() == null) return;
            if (onFailed.GetInvocationList().Length == 0) return;

            foreach (Delegate action in onFailed.GetInvocationList())
                onFailed -= (Action)action;
        }

        void IUpdatable.update()
        {
            if (destinationTile != null && destination != new Vector2(-1, -1))
            {
                Tile currentTile = WorldController.GetTileAt((int) entity.position.X / 100, (int) entity.position.Y / 100);
                
                // If our pathfinding is null lets just give up
                if (pathfind == null)
                {
                    destination = new Vector2(-1, -1);
                    destinationTile = null;
                    onFailed?.Invoke();
                    ClearHandles();
                    return;
                }

                // If we havent got a nextTile set
                if (nextTile == null && nextPosition == new Vector2(-1, -1) && pathfind.Length() > 0)
                {
                    nextTile = pathfind.Dequeue();
                    nextPosition = nextTile.GetPosition();
                    posLerpTime = 0f;
                    tileLerpTime = 0f;
                }

                if (nextTile != null)
                {
                    entity.position = new Vector2(Mathf.lerp(entity.position.X, nextTile.GetPosition().X, tileLerpTime), Mathf.lerp(entity.position.Y, nextTile.GetPosition().Y, tileLerpTime));
                    tileLerpTime += (mob.GetMovementSpeed() * 0.1f) * Time.deltaTime;

                    if (nextTile == currentTile)
                        nextTile = null;
                }

                if (nextTile == null & nextPosition != new Vector2(-1, -1))
                {
                    entity.position = new Vector2(Mathf.lerp(entity.position.X, nextPosition.X, posLerpTime), Mathf.lerp(entity.position.Y, nextPosition.Y, posLerpTime));
                    posLerpTime += (mob.GetMovementSpeed() * 0.5f) * Time.deltaTime;

                    if (new Rectangle((int) nextPosition.X - 5, (int) nextPosition.Y - 5, 10, 10).Contains(entity.position))
                        nextPosition = new Vector2(-1, -1);
                }

                if (currentTile == destinationTile & new Rectangle((int) destination.X - 5, (int) destination.Y - 5, 10, 10).Contains(entity.position))
                {
                    destinationTile = null;
                    pathfind = null;
                    onReached?.Invoke();
                    ClearHandles();
                }
            }
        }
    }
}
