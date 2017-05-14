using HospitalCeo.Building;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Console;
using System;
using System.Collections.Generic;

/*
 * Brett Taylor
 * Used when building in infrastructure - walls, roads, fences
 * Uses mainly the component Dragging
 */

namespace HospitalCeo.Zoning
{
    public class ZoneDraggingComponent : DraggingComponent
    {
        private Type zoneType;

        public ZoneDraggingComponent(Color drawingColor, bool shouldShowRulerLines, bool shouldShowRulerLengthText) : base(drawingColor, shouldShowRulerLines, shouldShowRulerLengthText)
        {

        }

        public void StartZoning(Type zoneType)
        {
            if (BuildingController.IsDragging() || IsDragging())
            {
                DebugConsole.instance.log("We are already building, placing or zoning.");
                return;
            }

            this.zoneType = zoneType;
            StartDragging(true);
        }

        // Called when the DraggingComponent has finished dragging
        public override void OnFinishedDragging()
        {
            zoneType = null;
        }

        // Called when the DraggingComponent has completed a successfull drag.
        public override void OnSuccessfullDrag(Vector2 tileNumber, Vector2 size, List<Tile> tilesAffected)
        {
            PlaceZone(tileNumber, size, tilesAffected);
        }

        public Zone PlaceZone(Type zone, Vector2 tilePosition, Vector2 size)
        {
            this.zoneType = zone;
            List<Tile> tilesAffected = new List<Tile>();

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    Tile t = WorldController.GetTileAt((int)tilePosition.X + x, (int)tilePosition.Y + y);
                    if (t == null) continue;
                    tilesAffected.Add(t);
                }
            }

            return PlaceZone(tilePosition, size, tilesAffected);
        }

        public Zone PlaceZone(Vector2 tilePosition, Vector2 size, List<Tile> tilesAffected)
        {
            foreach (Tile t in tilesAffected)
            {
                Zone zoneTile = t.GetZone();
                if (zoneTile != null) return null;
            }

            Zone zone = (Zone) Activator.CreateInstance(zoneType, tilePosition, size);
            Entity entity = WorldController.SCENE.createEntity("Zone: " + zone);
            entity.addComponent(zone);
            return zone;
        }
    }
}
