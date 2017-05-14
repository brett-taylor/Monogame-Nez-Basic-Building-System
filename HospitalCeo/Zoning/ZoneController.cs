using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using System;

/*
 * Brett Taylor
 * The zone controller
 */

namespace HospitalCeo.Zoning
{
    public static class ZoneController
    {
        private static Entity zoneBuilding;
        private static ZoneDraggingComponent zoneDraggingComponent;

        public static void Initialise()
        {
            // Create the infrastructure building entity
            zoneBuilding = WorldController.SCENE.createEntity("Zone Placing Entity");
            zoneDraggingComponent = zoneBuilding.addComponent<ZoneDraggingComponent>(new ZoneDraggingComponent(new Color(Color.Green, 0.2f), true, true));
        }

        public static bool IsDragging()
        {
            return zoneDraggingComponent.IsDragging();
        }

        public static void StartZoning(Type zoneType)
        {
            zoneDraggingComponent.StartZoning(zoneType);
        }

        public static Zone PlaceZone(Type zoneType, Vector2 tilePosition, Vector2 tileSize)
        {
            return zoneDraggingComponent.PlaceZone(zoneType, tilePosition, tileSize);
        }
    }
}
