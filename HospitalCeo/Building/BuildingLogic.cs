using System.Collections.Generic;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using HospitalCeo.World;
using HospitalCeo.Tasks;
using System;

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
            renderer = (BuildingBaseRenderer) System.Activator.CreateInstance(type, this);
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
            for (int x = 0; x < tileSize.X; x++)
            {
                for (int y = 0; y < tileSize.Y; y++)
                {
                    Tile t = WorldController.GetTileAt((int)tilePosition.X + x, (int)tilePosition.Y + y);
                    Task task = new Task(t, .5f, onComplete => {});

                    task.RegisterTaskUpdate(
                        onUpdate =>
                        {
                            int percentage = (int) (100 - (onUpdate.GetTimeLeft() / onUpdate.GetTimeMaximum()) * 100);
                            BuildingSprite sprite = renderer.GetSpriteAt(onUpdate.GetTile().GetTileNumber());

                            if (sprite != null) sprite.SetPercentageBuilt(percentage);
                        });

                    TaskManager.WORKMAN_TASK_QUEUE.Enqueue(task);
                }
            }
        }
    }
}
