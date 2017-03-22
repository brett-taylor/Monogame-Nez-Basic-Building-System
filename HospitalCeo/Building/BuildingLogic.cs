using System.Collections.Generic;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using HospitalCeo.World;

/*
 * Brett Taylor
 * The generic layout for all building components
 */

namespace HospitalCeo.Building
{
    public abstract class BuildingLogic : Component, IBuildingInformation
    {
        // Variables
        protected Sprite sprite;
        protected List<Tile> tiles;
        protected Vector2 tileSize;
        protected Vector2 tilePosition;

        // IBuildingInformation Interface
        public abstract string GetName();
        public abstract Subtexture GetTexture();
        public abstract BuildingType GetBuildingType();
        public abstract BuildingCategory GetBuildingCatergory();
        public abstract Vector2 GetTileSize();

        public virtual bool UsesCustomRenderer()
        {
            return false;
        }

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

        public void CreateRenderer()
        {
            entity.addComponent(new BuildingRepeatedTiledRenderer());
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
    }
}
