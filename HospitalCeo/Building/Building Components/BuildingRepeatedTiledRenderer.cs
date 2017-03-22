using System.Collections.Generic;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class BuildingRepeatedTiledRenderer : Component
    {
        private Sprite[,] sprites;
        private BuildingLogic building;
        private Subtexture texture;

        public override void onAddedToEntity()
        {
            building = entity.getComponent<BuildingLogic>();
            texture = building.GetTexture();
            sprites = new Sprite[(int) building.GetTileSize().X, (int) building.GetTileSize().Y];

            for (int x = 0; x < building.GetTileSize().X; x++)
            {
                for (int y = 0; y < building.GetTileSize().Y; y++)
                {
                    Sprite newSprite = new Sprite(texture);
                    newSprite.localOffset = new Vector2(x * 100, y * 100);
                    newSprite.renderLayer = 18;
                    entity.addComponent<Sprite>(newSprite);
                }
            }
        }
    }
}
