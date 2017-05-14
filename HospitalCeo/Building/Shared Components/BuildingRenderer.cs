using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;
using Nez.Sprites;

namespace HospitalCeo.Building.Shared_Components
{
    public class BuildingRenderer : Component
    {
        protected BuildingType buildingType;
        protected Building building;
        protected Subtexture texture;
        protected Sprite[,] sprites;

        public BuildingRenderer(BuildingType buildingType, Building building, Subtexture texture)
        {
            this.buildingType = buildingType;
            this.building = building;
            this.texture = texture;
            building.components.Add("renderer", this);
        }

        public override void onAddedToEntity()
        {
            int renderLayer = building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure ? Utils.RenderLayers.BUILDING_INFRASTRUCTURE : Utils.RenderLayers.BUILDING_GAMEPLAY;
            if (buildingType == BuildingType.Infrastructure)
            {
                sprites = new Sprite[(int) building.GetTileSize().X, (int) building.GetTileSize().Y];

                for (int x = 0; x < building.GetTileSize().X; x++)
                {
                    for (int y = 0; y < building.GetTileSize().Y; y++)
                    {
                        Sprite sprite = new Sprite(texture);
                        sprite.localOffset = new Vector2(x * 100, y * 100);

                        sprite.renderLayer = renderLayer;
                        entity.addComponent(sprite);
                        sprites[x, y] = sprite;
                    }
                }
            }
            else
            {
                sprites = new Sprite[1, 1];
                Sprite sprite = new Sprite(texture);
                sprite.localOffset = new Vector2(0, 0);
                sprite.renderLayer = renderLayer;
                entity.addComponent(sprite);
                sprites[0, 0] = sprite;
            }
        }

        public override void onEnabled()
        {
            if (buildingType == BuildingType.Infrastructure)
            {
                foreach (Sprite s in sprites)
                    s?.setEnabled(true);
            }
            else
                sprites[0, 0]?.setEnabled(true);
        }

        public override void onDisabled()
        {
            if (buildingType == BuildingType.Infrastructure)
            {
                foreach (Sprite s in sprites)
                    s?.setEnabled(false);
            }
            else
                sprites[0, 0]?.setEnabled(false);
        }

        public void ToggleSpriteAtPosition(Vector2 tilePosition, bool isWorldSpace)
        {
            if (buildingType == BuildingType.Infrastructure)
            {
                Sprite s = GetSpriteAt(tilePosition, isWorldSpace);
                s?.setEnabled(!s.enabled);
            }
            else
                sprites[0, 0]?.setEnabled(!sprites[0, 0].enabled);
        }

        public Sprite GetSpriteAt(Vector2 tilePosition, bool isWorldSpace)
        {
            if (buildingType == BuildingType.Infrastructure)
            {
                if (isWorldSpace)
                    tilePosition = building.WorldSpaceToLocalSpace(tilePosition);

                Sprite s = sprites[(int)tilePosition.X, (int)tilePosition.Y];
                return s == null ? null : s;
            }
            else
                return sprites[0, 0] == null ? null : sprites[0, 0];
        }
    }
}
