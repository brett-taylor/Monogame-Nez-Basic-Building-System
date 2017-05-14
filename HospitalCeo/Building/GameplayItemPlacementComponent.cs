using HospitalCeo.Building.Gameplay.Interfaces;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;

namespace HospitalCeo.Building
{
    public class GameplayItemPlacementComponent : Component
    {
        private PrimitiveBuilding primitiveBuilding;
        private PlacingComponent placingComponent;
        private Entity entitySprite;
        private Sprite sprite;
        private Tile lastTile;

        public GameplayItemPlacementComponent()
        {
            placingComponent = new PlacingComponent();
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            entity.addComponent(placingComponent);

            Action<Tile> placementMoved = (Tile t) => PlacementMoved(t);
            placingComponent.RegisterOnPlacementCancelled(PlacementCancelled);
            placingComponent.RegisterOnPlacementSuccessfull(PlacementSuccesfull);
            placingComponent.RegisterOnPlacementMoved(placementMoved);
            placingComponent.RegisterOnPlacementCompleted(PlacementCompleted);
        }

        public void StartPlacing(PrimitiveBuilding primitiveBuilding)
        {
            if (BuildingController.IsDragging() || Zoning.ZoneController.IsDragging())
            {
                Nez.Console.DebugConsole.instance.log("We are already building, placing or zoning.");
                return;
            }

            this.primitiveBuilding = primitiveBuilding;
            placingComponent.StartPlacing(primitiveBuilding.GetSize(), primitiveBuilding.CarryOnBuildingAfterPlacement(), false);

            if (entitySprite == null)
            {
                entitySprite = WorldController.SCENE.createEntity("GameplayItemPlacement sprite");
                sprite = new Sprite();
                entitySprite.addComponent(sprite);
            }

            entitySprite.setEnabled(true);
            sprite.setSubtexture((primitiveBuilding as IBuildingStandardGameplayRenderer).GetTexture());
            sprite.setLocalOffset(new Vector2(sprite.width / 2, 0) - new Vector2(50, 0));
            placingComponent.ShouldCarryOnAfterPlaced(primitiveBuilding.CarryOnBuildingAfterPlacement());
        }

        private void PlacementCompleted()
        {
            if (!placingComponent.IsCarryingOnAfterPlaced())
            {
                entitySprite.setEnabled(false);
                lastTile = null;
            }
        }

        private void PlacementSuccesfull()
        {
            Tile t = WorldController.GetMouseOverTile();
            Entity newObjectEntity = WorldController.SCENE.createEntity("Building at x: " + t.GetPosition().X + " y: " + t.GetPosition().Y);
            List<Tile> tilesAffected = new List<Tile>();

            for (int x = 0; x < primitiveBuilding.GetSize().X; x++)
            {
                for (int y = 0; y < primitiveBuilding.GetSize().Y; y++)
                {
                    Tile tile = WorldController.GetTileAt((int) lastTile.GetTileNumber().X + x, (int) lastTile.GetTileNumber().Y + y);
                    if (tile != null)
                        tilesAffected.Add(tile);
                }
            }

            Building building = new Building();
            newObjectEntity.addComponent(building);
            building.SetPrimitiveBuilding(primitiveBuilding);
            building.AttachToTile(tilesAffected);
            building.SetPosition(entitySprite.position + sprite.localOffset, true);
            building.SetTileSize(primitiveBuilding.GetSize());
        }

        private void PlacementMoved(Tile t)
        {
            entitySprite.position = t.GetPosition();
            lastTile = t;
        }

        private void PlacementCancelled()
        {
        }

        public bool IsPlacing()
        {
            return placingComponent.IsPlacing();
        }
    }
}
