using System;
using Nez;
using HospitalCeo.World;
using Microsoft.Xna.Framework;

/*
 * Brett Taylor
 * Holds all of the entities related to building - infrastructure building, gameplay object building, zone drawing
 */

namespace HospitalCeo.Building
{
    public static class BuildingController
    {
        private static Entity infrastructureBuilding;
        private static InfrastructureBuildingComponent infrastructureBuildingComponent;

        public static void Initialise()
        {
            // Create the infrastructure building entity
            infrastructureBuilding = WorldController.SCENE.createEntity("Infrastructure building entity");
            infrastructureBuildingComponent = infrastructureBuilding.addComponent<InfrastructureBuildingComponent>(new InfrastructureBuildingComponent(new Color(Color.Green, 0.2f), true, true));
        }

        public static void StartBuilding(Type buildingLogic)
        {
            infrastructureBuildingComponent.StartBuilding(buildingLogic);
        }

        public static bool IsDragging()
        {
            return infrastructureBuildingComponent.IsDragging();
        }

        public static BuildingLogic PlaceBuilding(Type buildingType, Vector2 tilePosition, Vector2 tileSize, BuildingCategory[] extraCategories = null)
        {
            return infrastructureBuildingComponent.PlaceBuilding(buildingType, tilePosition, tileSize, extraCategories);
        }
    }
}
