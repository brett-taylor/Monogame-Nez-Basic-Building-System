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
        private Type buildingLogic;

        public InfrastructureBuildingComponent(Color drawingColor, bool shouldShowRulerLines, bool shouldShowRulerLengthText) : base(drawingColor, shouldShowRulerLines, shouldShowRulerLengthText)
        {
        }

        public void StartBuilding(Type logic)
        {
            if (IsDragging())
            {
                DebugConsole.instance.log("We are already building");
                return;
            }

            buildingLogic = logic;
            BuildingLogic actualBuilding = (BuildingLogic) Activator.CreateInstance(buildingLogic);
            StartDragging(actualBuilding.CarryOnBuildingAfterBuild());
            actualBuilding = null;
        }

        // Called when the DraggingComponent has finished dragging
        public override void OnFinishedDragging()
        {
            buildingLogic = null;
        }

        // Called when the DraggingComponent has completed a successfull drag.
        public override void OnSuccessfullDrag(Vector2 tileNumber, Vector2 size, List<Tile> tilesAffected)
        {
            PlaceBuilding(tileNumber, size, tilesAffected);
        }

        public void PlaceBuilding(Type building, Vector2 tilePosition, Vector2 size)
        {
            this.buildingLogic = building;
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

            PlaceBuilding(tilePosition, size, tilesAffected);
        }

        public void PlaceBuilding(Vector2 tilePosition, Vector2 size, List<Tile> tilesAffected)
        {
            if (buildingLogic == null) return;
            if (tilesAffected == null || tilesAffected.Count == 0) return;

            foreach (Tile t in tilesAffected)
            {
                if (t.GetInfrastructureItem() != null) return;
            }

            // Create the building logic and then run OnBeforeBuild
            BuildingLogic logic = (BuildingLogic)Activator.CreateInstance(buildingLogic);
            IBuildingBeforeBuild logicBeforeBuild = logic as IBuildingBeforeBuild;
            if (logicBeforeBuild != null)
            {
                logicBeforeBuild.OnBeforeBuild(tilePosition, size);

                // If the building doesnt actually want to be built
                if (!logicBeforeBuild.ShouldActuallyBuild())
                {
                    logic = null;
                    logicBeforeBuild = null;
                    return;
                }
            }

            // Create Entity
            Entity newBuilding = WorldController.SCENE.createEntity("Building at x: " + tilePosition.X + " y: " + tilePosition.Y);

            // Set up the Building logic
            newBuilding.addComponent(logic);
            logic.AttachToTile(tilesAffected);
            logic.SetPosition(tilesAffected[0].GetPosition());
            logic.SetTilePosition(tilePosition);
            logic.SetTileSize(size);

            // If the logic has IBuildingOnBuild then call OnBuild
            IBuildingOnBuild logicOnBuild = logic as IBuildingOnBuild;
            if (logicOnBuild != null)
                logicOnBuild.OnBuild();

            // If the logic doesnt have has IBuildingCustomRenderer on it then create the default renderer otherwise add the custom one
            IBuildingCustomRenderer customRenderer = logic as IBuildingCustomRenderer;
            if (customRenderer == null)
                logic.CreateRenderer();
            else
                customRenderer.DoCustomRenderer();
        }
    }
}
