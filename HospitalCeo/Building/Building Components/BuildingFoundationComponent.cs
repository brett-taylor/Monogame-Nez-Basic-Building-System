using System;
using Microsoft.Xna.Framework;
using Nez;
using HospitalCeo.World;
using HospitalCeo.Building;

namespace HospitalCeo.Building
{
    public class BuildingFoundationComponent : Component
    {
        private Type wallType;
        private Type floorType;
        private Vector2 tilePosition;
        private Vector2 tileSize;

        public BuildingFoundationComponent(Type wall, Type floor, Vector2 pos, Vector2 size)
        {
            this.wallType = wall;
            this.floorType = floor;
            this.tilePosition = pos;
            this.tileSize = size;  
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            CreateFoundation();
        }

        private void CreateFoundation()
        {
            // Create the Top, Bottom, Left, Right walls
            BuildingLogic northWall = BuildingController.PlaceBuilding(wallType, tilePosition, new Vector2(tileSize.X, 1));
            BuildingLogic leftWall = BuildingController.PlaceBuilding(wallType, new Vector2(tilePosition.X, tilePosition.Y + 1), new Vector2(1, tileSize.Y - 2));
            BuildingLogic rightWall = BuildingController.PlaceBuilding(wallType, new Vector2(tilePosition.X + tileSize.X - 1, tilePosition.Y + 1), new Vector2(1, tileSize.Y - 2));
            BuildingLogic bottomWall = BuildingController.PlaceBuilding(wallType, new Vector2(tilePosition.X, tilePosition.Y + tileSize.Y - 1), new Vector2(tileSize.X, 1));

            // Create the flooring
            BuildingLogic flooring = BuildingController.PlaceBuilding(floorType, new Vector2(tilePosition.X + 1, tilePosition.Y + 1), new Vector2(tileSize.X - 2, tileSize.Y - 2), extraCategories: new[] { BuildingCategory.Wall });

            // Destory the entity
            entity.destroy();
        }
    }
}
