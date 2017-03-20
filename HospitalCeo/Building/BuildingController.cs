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
            infrastructureBuildingComponent = infrastructureBuilding.addComponent<InfrastructureBuildingComponent>(new InfrastructureBuildingComponent(Color.Blue, true, true, true));
        }

        public static void StartBuilding(Type buildingType)
        {
            infrastructureBuildingComponent.StartBuilding(buildingType);
        }

        public static void FinishBuilding()
        {

        }
    }
}
