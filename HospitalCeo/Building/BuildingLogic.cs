using System.Collections.Generic;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using HospitalCeo.World;
using System;
using HospitalCeo.Tasks;

/*
 * Brett Taylor
 * The generic layout for all building components
 */

namespace HospitalCeo.Building
{
    public abstract class BuildingLogic : Component, IBuildingInformation
    {
        // Variables
        protected List<Tile> tiles;
        protected Vector2 tileSize;
        protected Vector2 tilePosition;
        protected BuildingBaseRenderer renderer;

        // IBuildingInformation Interface
        public abstract string GetName();
        public abstract Subtexture GetTexture();
        public abstract BuildingType GetBuildingType();
        public abstract BuildingCategory GetBuildingCatergory();
        public abstract Vector2 GetTileSize();

        public virtual bool IsOneSquareWidth()
        {
            return false;
        }

        public virtual bool CarryOnBuildingAfterBuild()
        {
            return true;
        }

        public virtual int GetMovementCost()
        {
            return 1;
        }

        public Entity GetEntity()
        {
            return entity;
        }

        public List<Tile> GetTiles()
        {
            return tiles;
        }

        public void SetTileSize(Vector2 size)
        {
            this.tileSize = size;
        }

        public void AttachToTile(List<Tile> tiles)
        {
            this.tiles = tiles;

            foreach (Tile t in tiles)
            {
                t.SetInfrastructureItem(this);
            }
        }

        public void CreateRenderer(System.Type type)
        {
            renderer = (BuildingBaseRenderer)System.Activator.CreateInstance(type, this);
            entity.addComponent(renderer);
        }

        public BuildingBaseRenderer GetRenderer()
        {
            return renderer;
        }

        public void SetPosition(Vector2 positon)
        {
            if (entity == null)
            {
                Nez.Console.DebugConsole.instance.log(entity + " has not attached entity - could not set position");
                Nez.Console.DebugConsole.instance.Open();
                return;
            }

            entity.transform.position = positon;
        }

        public void SetTilePosition(Vector2 tiles)
        {
            tilePosition = tiles;
        }

        public Vector2 GetTilePosition()
        {
            return tilePosition;
        }

        public void StartConstruction()
        {
            Task building = new Task();
            Subtask constructBuilding = new Subtask();
            building.AddSubtask(constructBuilding);

            for (int x = 0; x < tileSize.X; x++)
            {
                for (int y = 0; y < tileSize.Y; y++)
                {
                    // Grab the tile
                    Tile t = WorldController.GetTileAt((int)tilePosition.X + x, (int)tilePosition.Y + y);
                    Instruction instruction = new Instruction();

                    // Create the wait At tile for 2 seconds process
                    Process waitAtTile = new Process(t, 1f, onComplete => { });
                    instruction.AddProcess(waitAtTile);

                    Process turnRed = new Process(t, 0.01f, onComplete => {
                        //onComplete.GetInstruction().GetEntity().getComponent<AI.MobSwapSpriteRenderer>().color = Color.Red;*
                    });
                    instruction.AddProcess(turnRed);

                    Process faceNorth = new Process(t, 0.01f, onComplete => {
                        //onComplete.GetInstruction().GetEntity().getComponent<AI.MobSwapSpriteRenderer>().setSubtexture(onComplete.GetInstruction().GetEntity().getComponent<AI.Staff.Staff>().GetNorthFacingSprite());
                    });
                    instruction.AddProcess(faceNorth);

                    // Create the actual build
                    Process constructProcess = new Process(t, .5f, onComplete => { });
                    instruction.AddProcess(constructProcess);
                    constructProcess.RegisterOnTickHandle(onUpdate =>
                    {
                        int percentage = (int)(100 - (onUpdate.GetRemainingTime() / onUpdate.GetProcessTime()) * 100);
                        BuildingSprite sprite = renderer.GetSpriteAt(onUpdate.GetWorkTile().GetTileNumber());
                        if (sprite != null) sprite.SetPercentageBuilt(percentage);
                    }
                    );

                    Process turnWhite = new Process(t, 0.01f, onComplete =>
                    {
                        //onComplete.GetInstruction().GetEntity().getComponent<AI.MobSwapSpriteRenderer>().color = Color.White;
                        WorldController.PATHFINDING_HUMAN_GRID.RebuildTile(onComplete.GetWorkTile());
                    });
                    instruction.AddProcess(turnWhite);

                    constructBuilding.AddInstruction(instruction);
                }
            }
        }
    }
}
