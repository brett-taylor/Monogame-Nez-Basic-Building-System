using System;
using System.Collections.Generic;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Console;

/*
 * Brett Taylor
 * Used when building in infrastructure - walls, roads, fences
 * Uses mainly the component Dragging
 */

namespace HospitalCeo.Building
{
    public class InfrastructureBuildingComponent : DraggingComponent
    {
        private Type buildingType;

        public InfrastructureBuildingComponent(Color drawingColor, bool shouldShowRulerLines, bool shouldShowRulerLengthText, bool carryOnAfterSuccessfullDraw) : base(drawingColor, shouldShowRulerLines, shouldShowRulerLengthText, carryOnAfterSuccessfullDraw)
        {
        }

        public void StartBuilding(Type type)
        {
            if (IsDragging() || buildingType != null)
            {
                DebugConsole.instance.log("We are already building");
                return;
            }

            buildingType = type;
            StartDragging();
        }

        // Called when the DraggingComponent has finished dragging
        public override void OnFinishedDragging()
        {
            buildingType = null;
        }

        // Called when the DraggingComponent has completed a successfull drag.
        public override void OnSuccessfullDrag(Vector2 tileNumber, Vector2 size)
        {
            if (buildingType == null) return;

            if (buildingType == typeof(Foundation))
            {
                FoundationCreate(tileNumber, size);
                return;
            }

            List<Building> listOfNewBuildings = new List<Building>();

            // Place the building
            for (int x = 0; x < (size.X); x++)
            {
                for (int y = 0; y < (size.Y); y++)
                {
                    Tile t = WorldController.GetTileAt((int) tileNumber.X + x, (int) tileNumber.Y + y);
                    if (t == null) continue;
                    if (t.GetInfrastructureItem() != null) continue;

                    Building tempBuilding = (Building)Activator.CreateInstance(buildingType, t.position);
                    tempBuilding.OnBuildingCompleted();
                    t.SetInfrastructureItem(tempBuilding);
                    tempBuilding.OnBuildingCompleted();

                    // If the building has a custom method to set its sprites fully then add it to a list.
                    if (tempBuilding.CustomSpriteSettings())
                    {
                        listOfNewBuildings.Add(tempBuilding);
                    }
                }
            }

            // This is used for buildings that sprites change depending on certain things.
            // E.g. walls change on their surrounding tiles
            // Call that building to update its sprite
            foreach (Building building in listOfNewBuildings)
            {
                building.SetSpritesCorrectly();
            }
        }

        // If we were building the foundation then change it to flooring - walls
        private void FoundationCreate(Vector2 actualPosition, Vector2 actualSize)
        {
            List<BaseWall> newWalls = new List<BaseWall>();

            // Build the Top / Bottom wall
            for (int x = 0; x < actualSize.X; x++)
            {
                // Top Wall
                Tile topTile = WorldController.GetTileAt((int)actualPosition.X + x, (int)actualPosition.Y);
                if (topTile != null)
                {
                    if (topTile.GetInfrastructureItem() == null)
                    {
                        BaseWall wall = new BaseWall(topTile.position);
                        newWalls.Add(wall);
                        topTile.SetInfrastructureItem(wall);
                    }
                }

                // Bottom Wall
                Tile bottomTile = WorldController.GetTileAt((int)actualPosition.X + x, (int)actualPosition.Y + (int)actualSize.Y - 1);
                if (bottomTile != null)
                {
                    if (bottomTile.GetInfrastructureItem() == null)
                    {
                        BaseWall wall = new BaseWall(bottomTile.position);
                        newWalls.Add(wall);
                        bottomTile.SetInfrastructureItem(wall);
                    }
                }
            }

            // Build the Left / Right Wall
            for (int y = 0; y < actualSize.Y; y++)
            {
                // Left Wall
                Tile leftTile = WorldController.GetTileAt((int)actualPosition.X, (int)actualPosition.Y + y);
                if (leftTile != null)
                {
                    if (leftTile.GetInfrastructureItem() == null)
                    {
                        BaseWall wall = new BaseWall(leftTile.position);
                        newWalls.Add(wall);
                        leftTile.SetInfrastructureItem(wall);
                    }
                }

                // Right Wall
                Tile rightTile = WorldController.GetTileAt((int)actualPosition.X + (int)actualSize.X - 1, (int)actualPosition.Y + y);
                if (rightTile != null)
                {
                    if (rightTile.GetInfrastructureItem() == null)
                    {
                        BaseWall wall = new BaseWall(rightTile.position);
                        newWalls.Add(wall);
                        rightTile.SetInfrastructureItem(wall);
                    }
                }
            }

            // Build the floor
            for (int x = 1; x < actualSize.X - 1; x++)
            {
                for (int y = 1; y < actualSize.Y - 1; y++)
                {
                    Tile tile = WorldController.GetTileAt((int)actualPosition.X + x, (int)actualPosition.Y + y);
                    if (tile != null)
                    {
                        if (tile.GetGameplayItem() == null)
                        {
                            FlooringCheapConcrete flooring = new FlooringCheapConcrete(tile.position);
                        }
                    }
                }
            }

            // Set up the wall sprites
            foreach (Building wall in newWalls)
            {
                wall.SetSpritesCorrectly();
            }
        }
    }
}
