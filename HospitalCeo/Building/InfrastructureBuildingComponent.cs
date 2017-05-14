using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Console;
using System.Collections.Generic;

/*
 * Brett Taylor
 * Used when building in infrastructure - walls, roads, fences
 * Uses mainly the component Dragging
 */

namespace HospitalCeo.Building
{
    public class InfrastructureBuildingComponent : DraggingComponent
    {
        private PrimitiveBuilding primitivebuilding;

        public InfrastructureBuildingComponent(Color drawingColor, bool shouldShowRulerLines, bool shouldShowRulerLengthText) : base(drawingColor, shouldShowRulerLines, shouldShowRulerLengthText)
        {
        }

        public void StartBuilding(PrimitiveBuilding primitivebuilding, bool shouldCarryOnAfterDragging)
        {
            if (BuildingController.IsDragging() || Zoning.ZoneController.IsDragging())
            {
                Nez.Console.DebugConsole.instance.log("We are already building, placing or zoning.");
                return;
            }

            this.primitivebuilding = primitivebuilding;
            StartDragging(shouldCarryOnAfterDragging);
        }

        // Called when the DraggingComponent has finished dragging
        public override void OnFinishedDragging()
        {
            primitivebuilding = null;
        }

        // Called when the DraggingComponent has completed a successfull drag.
        public override void OnSuccessfullDrag(Vector2 tileNumber, Vector2 size, List<Tile> tilesAffected)
        {
            PlaceBuilding(tileNumber, size, tilesAffected);
        }

        public Building PlaceBuilding(PrimitiveBuilding primitivebuilding, Vector2 tilePosition, Vector2 size)
        {
            this.primitivebuilding = primitivebuilding;
            List<Tile> tilesAffected = new List<Tile>();

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    Tile t = WorldController.GetTileAt((int) tilePosition.X + x, (int) tilePosition.Y + y);
                    if (t == null) continue;
                    tilesAffected.Add(t);
                }
            }

            return PlaceBuilding(tilePosition, size, tilesAffected);
        }

        public Building PlaceBuilding(Vector2 tilePosition, Vector2 size, List<Tile> tilesAffected)
        {
            if (primitivebuilding == null) return null;
            if (tilesAffected == null || tilesAffected.Count == 0) return null;

            foreach (Tile t in tilesAffected)
            {
                Building b = t.GetInfrastructureItem();
                if (b != null) return null;
            }

            Entity entity = WorldController.SCENE.createEntity("Building at x: " + tilePosition.X + " y: " + tilePosition.Y);

            // Set up the Building logic
            Building building = new Building();
            entity.addComponent(building);
            building.SetPrimitiveBuilding(primitivebuilding);
            building.AttachToTile(tilesAffected);
            building.SetPosition(tilesAffected[0].GetPosition());
            building.SetTileSize(size);
            return building;
        }
    }
}
