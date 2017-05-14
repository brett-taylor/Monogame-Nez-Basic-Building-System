using HospitalCeo.Building.Gameplay.Components;
using HospitalCeo.Building.Gameplay.Interfaces;
using HospitalCeo.Tasks;
using Microsoft.Xna.Framework;
using Nez;

namespace HospitalCeo.Building.Shared_Components
{
    public class UnderConstruction : Component
    {
        private ConstructionRenderer[,] constructionRenderers;
        private BuildingRenderer buildingsRenderer;
        private Building building;
        private float timeForEachJob;

        public UnderConstruction(Building building, float timeForEachJob)
        {
            this.building = building;
            this.timeForEachJob = timeForEachJob;
            building.components.Add("construction", this);
        }

        public override void onAddedToEntity()
        {
            buildingsRenderer = building.components["renderer"] as BuildingRenderer;
            buildingsRenderer?.setEnabled(false);

            if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure)
            {
                constructionRenderers = new ConstructionRenderer[(int) building.GetTileSize().X, (int) building.GetTileSize().Y];
                for (int x = 0; x < building.GetTileSize().X; x++)
                {
                    for (int y = 0; y < building.GetTileSize().Y; y++)
                    {
                        Vector2 offset = building.GetPosition() + new Vector2(x * 100, y * 100) - new Vector2(50, 50);
                        ConstructionRenderer cr = new ConstructionRenderer(offset);
                        cr.setLocalOffset(offset);
                        entity.addComponent(cr);
                        constructionRenderers[x, y] = cr;
                    }
                }
            }
            else
            {
                BuildingStandardGameplayRenderer renderer = building.components["renderer"] as BuildingStandardGameplayRenderer;
                IBuildingStandardGameplayRenderer iRenderer = building.GetPrimitiveObject() as IBuildingStandardGameplayRenderer;

                constructionRenderers = new ConstructionRenderer[1, 1];
                ConstructionRenderer cr = new ConstructionRenderer(entity.position - new Vector2(renderer.GetSpriteAt(new Vector2(0, 0), false).width / 2, renderer.GetSpriteAt(new Vector2(0, 0), false).height / 2));
                entity.addComponent(cr);
                cr.SetWidth(iRenderer.GetTexture().texture2D.Width);
                cr.SetHeight(iRenderer.GetTexture().texture2D.Height);
                constructionRenderers[0, 0] = cr;
            }

            AssignTask();
        }

        private void AssignTask()
        {
            Task parent = new Task();
            Subtask constructBuilding = new Subtask();
            parent.AddSubtask(constructBuilding);

            if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure)
            {
                for (int x = 0; x < building.GetTileSize().X; x++)
                {
                    for (int y = 0; y < building.GetTileSize().Y; y++)
                    {
                        Instruction workerTask = new Instruction();
                        World.Tile workTile = World.WorldController.GetTileAt(building.GetTilePosition() + new Vector2(x, y), isTileCoords: true);
                        if (workTile == null)
                            continue;

                        Process waitAtTile = new Process(workTile, 1f, onComplete => { });
                        workerTask.AddProcess(waitAtTile);

                        Process constructProcess = new Process(workTile, timeForEachJob, onComplete => {
                            Vector2 position = building.WorldSpaceToLocalSpace(onComplete.GetWorkTile().GetTileNumber());
                            RemoveConstructionRenderer(position);
                        });
                        constructProcess.RegisterOnTickHandle(onTick =>
                        {
                            float percentage = 80 - (onTick.GetRemainingTime() / onTick.GetProcessTime()) * 80;
                            Vector2 position = building.WorldSpaceToLocalSpace(onTick.GetWorkTile().GetTileNumber());
                            constructionRenderers[(int) position.X, (int) position.Y]?.SetPercentage(percentage);
                        });
                        workerTask.AddProcess(constructProcess);
                        constructBuilding.AddInstruction(workerTask);
                    }
                }
            }
            else
            {
                Instruction workerTask = new Instruction();
                World.Tile workTile = World.WorldController.GetTileAt(building.GetTilePosition(), isTileCoords: true);
                if (workTile == null)
                    return;

                Process waitAtTile = new Process(workTile, 1f, onComplete => { });
                workerTask.AddProcess(waitAtTile);

                Process constructProcess = new Process(workTile, timeForEachJob, onComplete => {
                    Vector2 position = building.WorldSpaceToLocalSpace(onComplete.GetWorkTile().GetTileNumber());
                    RemoveConstructionRenderer(position);
                });
                constructProcess.RegisterOnTickHandle(onTick =>
                {
                    float percentage = 80 - (onTick.GetRemainingTime() / onTick.GetProcessTime()) * 80;
                    Vector2 position = building.WorldSpaceToLocalSpace(onTick.GetWorkTile().GetTileNumber());
                    constructionRenderers[0, 0]?.SetPercentage(percentage);
                });
                workerTask.AddProcess(constructProcess);
                constructBuilding.AddInstruction(workerTask);
            }
        }

        public void RemoveConstructionRenderer(Vector2 position)
        {
            ConstructionRenderer cr;
            if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure)
                cr = constructionRenderers[(int)position.X, (int)position.Y];
            else
                cr = constructionRenderers[0, 0];

            entity.removeComponent(cr);
            constructionRenderers[(int) position.X, (int) position.Y] = null;
            building.FinishAtTile(position, false);

            foreach (ConstructionRenderer constructionRender in constructionRenderers)
            {
                if (constructionRender != null)
                    return;
            }

            entity.removeComponent(this);
        }

        public bool IsTileBuilt(Vector2 position, bool isWorldSpace)
        {
            if (isWorldSpace)
                position = building.WorldSpaceToLocalSpace(position);

            ConstructionRenderer cr;
            if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure)
                cr = constructionRenderers[(int) position.X, (int) position.Y];
            else
                cr = constructionRenderers[0, 0];

            if (cr == null)
                return true;
            else
                return false;
        }

        public override void onRemovedFromEntity()
        {
            Destory();
            base.onRemovedFromEntity();
        }

        private void Destory()
        {
            foreach (Component cr in constructionRenderers)
            {
                entity.removeComponent<ConstructionRenderer>();
            }

            building.components.Remove("construction");
            buildingsRenderer = null;
            building = null;
            constructionRenderers = null;
        }
    }
}
