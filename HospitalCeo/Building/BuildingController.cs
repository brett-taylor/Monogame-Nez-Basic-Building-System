using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using System;

/*
 * Brett Taylor
 * Holds all of the entities related to building - infrastructure building, gameplay object building, zone drawing
 */

namespace HospitalCeo.Building
{
    public static class BuildingController
    {
        private static Entity buildingEntity;
        private static InfrastructureBuildingComponent infrastructureBuildingComponent;
        private static GameplayItemPlacementComponent gameplayItemPlacingComponent;

        public static void Initialise()
        {
            // Create the infrastructure & gameplay building entity
            buildingEntity = WorldController.SCENE.createEntity("Infrastructure building entity");
            infrastructureBuildingComponent = buildingEntity.addComponent(new InfrastructureBuildingComponent(new Color(Color.Green, 0.2f), true, true));
            gameplayItemPlacingComponent = buildingEntity.addComponent(new GameplayItemPlacementComponent());
        }

        public static void StartBuilding(PrimitiveBuilding primitiveBuilding)
        {
            if (primitiveBuilding.GetBuildingType() == BuildingType.Infrastructure)
                infrastructureBuildingComponent.StartBuilding(primitiveBuilding, true);
            else
                gameplayItemPlacingComponent.StartPlacing(primitiveBuilding);
        }

        public static bool IsDragging()
        {
            return infrastructureBuildingComponent.IsDragging() || gameplayItemPlacingComponent.IsPlacing();
        }

        public static Building PlaceBuilding(PrimitiveBuilding primitiveBuilding, Vector2 tilePosition, Vector2 tileSize)
        {
            return infrastructureBuildingComponent.PlaceBuilding(primitiveBuilding, tilePosition, tileSize);
        }
    }
}
