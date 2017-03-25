using System;
using System.Collections.Generic;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class BuildingBaseRenderer : Component
    {
        protected BuildingLogic building;
        protected BuildingSprite[,] sprites;
        protected Subtexture texture;

        public BuildingBaseRenderer(BuildingLogic building)
        {
            this.building = building;
            sprites = new BuildingSprite[(int) building.GetTileSize().X, (int) building.GetTileSize().Y];
            texture = building.GetTexture();
        }

        public BuildingSprite GetSpriteAt(Vector2 position)
        {
            Vector2 spritePosition = position - building.GetTilePosition();
            if (spritePosition.X > sprites.GetUpperBound(0) || spritePosition.Y > sprites.GetUpperBound(1))
            {
                System.Diagnostics.Debug.WriteLine("Out of bounds BuildingRepeatedTileRenderer.GetSpriteAt() :" + position);
                return null;
            }

            return sprites[(int) spritePosition.X, (int) spritePosition.Y];
        }

        public BuildingSprite[,] GetAllSprites()
        {
            return sprites;
        }
    }
}
