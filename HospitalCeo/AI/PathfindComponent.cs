using HospitalCeo.Pathfinding;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using System;

/*
* Brett Taylor
* Pathfinding component added to entities
* Gives the component a destination tile
*/

namespace HospitalCeo.AI
{
    public class PathfindComponent : Component, IUpdatable
    {
        public static bool SHOULD_DRAW_PATHFIND_LINE = false;
        private Color color;

        private Mob mob;
        private Tile[] tilePath;
        private Tile nextTile;
        private Tile destinationTile;
        private int currentTileElement;
        private float posLerpTime;
        private float tileLerpTime;
        private bool alreadyAtDestination = false;
        private Vector2 destination = new Vector2(-1, -1);
        private Vector2 nextPosition = new Vector2(-1, -1);
        private Action onReached;
        private Action onFailed;

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

            if (currentTile == destinationTile)
            {
                alreadyAtDestination = true;
                return;
            }
            else
            {
                alreadyAtDestination = false;
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
            color = new Color(Nez.Random.range(0, 255), Nez.Random.range(0, 255), Nez.Random.range(0, 255));
            PathfindManager.RequestPathfind(PathfindPriority.Normal, this, currentTile, destinationTile);
        }

        public void AllowedToPathfind(Tile[] tiles)
        {
            tilePath = tiles;
        }

        public void RegisterOnReachHandle(Action action)
        {
            if (alreadyAtDestination)
            {
                action?.Invoke();
                return;
            }
            
            onReached += action;
        }

        public void RegisterOnFailedHandle(Action action)
        {
            onFailed += action;
        }

        private void ClearOnReachedHandles()
        {
            if (onReached == null) return;
            if (onReached.GetInvocationList() == null) return;
            if (onReached.GetInvocationList().Length == 0) return;

            foreach (Delegate action in onReached.GetInvocationList())
                onReached -= (Action) action;
        }

        private void ClearOnFailedHandles()
        {
            if (onFailed == null) return;
            if (onFailed.GetInvocationList() == null) return;
            if (onFailed.GetInvocationList().Length == 0) return;

            foreach (Delegate action in onFailed.GetInvocationList())
                onFailed -= (Action) action;
        }

        private void DonePathfinding()
        {
            tilePath = null;
            nextTile = null;
            destinationTile = null;
            currentTileElement = 0;
            posLerpTime = 0f;
            tileLerpTime = 0f;
            destination = new Vector2(-1, -1);
            nextPosition = new Vector2(-1, -1);
            ClearOnReachedHandles();
            ClearOnFailedHandles();
        }

        void IUpdatable.update()
        {
            if (tilePath == null)
                return;
            
            Tile currentTile = WorldController.GetTileAt((int) entity.position.X / 100, (int) entity.position.Y / 100);

            // If we havent got a nextTile set
            if (nextTile == null && nextPosition == new Vector2(-1, -1))
            {
                if (currentTileElement < 0 || currentTileElement > tilePath.Length)
                {
                    Nez.Console.DebugConsole.instance.log("Current Tile Element is out of bounds compared to the tilePath - stopping pathfinding");
                    Nez.Console.DebugConsole.instance.Open();
                    DonePathfinding();
                    return;
                }

                nextTile = tilePath[currentTileElement];
                nextPosition = nextTile.GetPosition();
                currentTileElement++;
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

            if (currentTile == destinationTile & new Rectangle((int) destination.X - 10, (int) destination.Y - 10, 20, 20).Contains(entity.position))
            {
                onReached?.Invoke();
                DonePathfinding();
            }
        }

        public override void debugRender(Graphics graphics)
        {
            if (SHOULD_DRAW_PATHFIND_LINE && tilePath != null)
            {
                Tile lastTile = null;
                for (int i = 0; i < tilePath.Length; i++)
                {
                    if (lastTile != null)
                        graphics.batcher.drawLine(lastTile.GetPosition(), tilePath[i].GetPosition(), color, thickness: 5f);
                    lastTile = tilePath[i];
                }
            }
        }
    }
}
