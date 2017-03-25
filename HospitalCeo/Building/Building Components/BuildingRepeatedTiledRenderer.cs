using System;
using System.Collections.Generic;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class BuildingRepeatedTiledRenderer : BuildingBaseRenderer
    {
        public BuildingRepeatedTiledRenderer(BuildingLogic building) : base(building)
        {
            this.building = building;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            for (int x = 0; x < building.GetTileSize().X; x++)
            {
                for (int y = 0; y < building.GetTileSize().Y; y++)
                {
                    BuildingSprite newSprite = new BuildingSprite(texture, building.GetTilePosition() * 100 + new Vector2(x * 100, y * 100));
                    newSprite.localOffset = new Vector2(x * 100, y * 100);
                    newSprite.renderLayer = 18;
                    sprites[x, y] = newSprite;
                    entity.addComponent<Sprite>(newSprite);
                }
            }
        }
    }
}
