using HospitalCeo.Building.Infrastructure.Interfaces;
using HospitalCeo.Building.Shared_Components;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez.Textures;
using System;

namespace HospitalCeo.Building.Infrastructure.Components
{
    public class BuildingDynamicTextureRenderer : BuildingRenderer
    {
        private IBuildingDynamicTexture buildingDynamicTexture;

        public BuildingDynamicTextureRenderer(Building building, IBuildingDynamicTexture buildingDynamicTexture) : base(BuildingType.Infrastructure, building, buildingDynamicTexture.SpriteNameHorizontal())
        {
            this.buildingDynamicTexture = buildingDynamicTexture;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            Action<Vector2> onBuilt = (Vector2 pos) => SetSprites(pos);
            building.RegisterOnTileBuilt(onBuilt);
        }

        private void SetSprites(Vector2 worldTilePosition)
        {
            Vector2 localSpace = building.WorldSpaceToLocalSpace(worldTilePosition);
            Tile t = WorldController.GetTileAt(worldTilePosition, isTileCoords: true);
            sprites[(int)localSpace.X, (int)localSpace.Y].setSubtexture(WorkOutSprite(t.SimilarItemsAroundTile(building), t));

            // North
            SetOtherSprite(WorldController.GetTileAt((int) worldTilePosition.X, (int) worldTilePosition.Y - 1));
            // South
            SetOtherSprite(WorldController.GetTileAt((int) worldTilePosition.X, (int) worldTilePosition.Y + 1));
            // West
            SetOtherSprite(WorldController.GetTileAt((int) worldTilePosition.X - 1, (int) worldTilePosition.Y));
            //East
            SetOtherSprite(WorldController.GetTileAt((int) worldTilePosition.X + 1, (int) worldTilePosition.Y));
        }

        private void SetOtherSprite(Tile tile)
        {
            // Check Tile Is Null
            if (tile != null)
            {
                // Grab the building on that tile and check if null
                Building other = tile.GetInfrastructureItem();
                if (other != null)
                {
                    // Check if that building has a dnyamic sprite renderer
                    IBuildingDynamicTexture otherDynamicRenderer = other.GetPrimitiveObject() as IBuildingDynamicTexture;
                    if (otherDynamicRenderer != null)
                    {
                        // Check if ame categories and then update the building's sprite at that location.
                        if (other.GetPrimitiveObject().GetBuildingCategory() == building.GetPrimitiveObject().GetBuildingCategory())
                        {
                            // Now attempt to get that building's renderer
                            BuildingDynamicTextureRenderer otherRenderer = other.components["renderer"] as BuildingDynamicTextureRenderer;
                            if (otherRenderer != null)
                            {
                                int amount = tile.SimilarConstructedItemsAroundTile(this.building);
                                Sprite sprite = otherRenderer.GetSpriteAt(tile.GetTileNumber(), true);
                                if (sprite != null)
                                    sprite.setSubtexture(WorkOutSprite(amount, tile));
                            }
                        }
                    }
                }
            }
        }

        private Subtexture WorkOutSprite(int amountOfConnections, Tile tile)
        {
            if (amountOfConnections == 0)
            {
                return buildingDynamicTexture.SpriteNameHorizontal();
            }

            // If there is one wall around it
            if (amountOfConnections == 1)
            {
                // If the tile is to the north
                Tile northTile = tile.GetNeighbour(Compass.N);
                if (tile.SimilarItemNextToTile(Compass.N, this.building) && northTile.GetInfrastructureItem().IsConstructed(northTile.GetTileNumber(), true))
                {
                    return buildingDynamicTexture.SpriteNameSouthEnding();
                }

                // If the tile is to the east
                Tile eastTile = tile.GetNeighbour(Compass.E);
                if (tile.SimilarItemNextToTile(Compass.E, this.building) && eastTile.GetInfrastructureItem().IsConstructed(eastTile.GetTileNumber(), true))
                {
                    return buildingDynamicTexture.SpriteNameWestEnding();
                }

                // If the tile is to the south
                Tile southTile = tile.GetNeighbour(Compass.S);
                if (tile.SimilarItemNextToTile(Compass.S, this.building) && southTile.GetInfrastructureItem().IsConstructed(southTile.GetTileNumber(), true))
                {
                    return buildingDynamicTexture.SpriteNameNorthEnding();
                }

                // If the tile is to the west
                Tile westTile = tile.GetNeighbour(Compass.W);
                if (tile.SimilarItemNextToTile(Compass.W, this.building) && westTile.GetInfrastructureItem().IsConstructed(westTile.GetTileNumber(), true))
                {
                    return buildingDynamicTexture.SpriteNameEastEnding();
                }
            }

            // If there are two connections
            if (amountOfConnections == 2)
            {
                // If one of the tiles is north
                Tile parentNorthTile = tile.GetNeighbour(Compass.N);
                if (tile.SimilarItemNextToTile(Compass.N, this.building) && parentNorthTile.GetInfrastructureItem().IsConstructed(parentNorthTile.GetTileNumber(), true))
                {
                    //If the 2nd tile is to the east
                    Tile eastTile = tile.GetNeighbour(Compass.E);
                    if (tile.SimilarItemNextToTile(Compass.E, this.building) && eastTile.GetInfrastructureItem().IsConstructed(eastTile.GetTileNumber(), true))
                    {
                        return buildingDynamicTexture.SpriteNameLNorthEast();
                    }

                    //If the 2nd tile is to the south
                    Tile southTile = tile.GetNeighbour(Compass.S);
                    if (tile.SimilarItemNextToTile(Compass.S, this.building) && southTile.GetInfrastructureItem().IsConstructed(southTile.GetTileNumber(), true))
                    {
                        return buildingDynamicTexture.SpriteNameVertical();
                    }

                    //If the 2nd tile is to the west
                    Tile westTile = tile.GetNeighbour(Compass.W);
                    if (tile.SimilarItemNextToTile(Compass.W, this.building) && westTile.GetInfrastructureItem().IsConstructed(westTile.GetTileNumber(), true))
                    {
                        return buildingDynamicTexture.SpriteNameLNorthWest();
                    }
                }

                // If one of the tiles is to the east
                Tile parentEastTile = tile.GetNeighbour(Compass.E);
                if (tile.SimilarItemNextToTile(Compass.E, this.building) && parentEastTile.GetInfrastructureItem().IsConstructed(parentEastTile.GetTileNumber(), true))
                {
                    // If the 2nd tile is to the west
                    Tile westTile = tile.GetNeighbour(Compass.W);
                    if (tile.SimilarItemNextToTile(Compass.W, this.building) && westTile.GetInfrastructureItem().IsConstructed(westTile.GetTileNumber(), true))
                    {
                        return buildingDynamicTexture.SpriteNameHorizontal();
                    }

                    // If the 2nd tile is to the south
                    Tile southTile = tile.GetNeighbour(Compass.S);
                    if (tile.SimilarItemNextToTile(Compass.S, this.building) && southTile.GetInfrastructureItem().IsConstructed(southTile.GetTileNumber(), true))
                    {
                        return buildingDynamicTexture.SpriteNameLEastSouth();
                    }
                }

                // If one of the tiles is to the south
                Tile parentSouthTile = tile.GetNeighbour(Compass.S);
                if (tile.SimilarItemNextToTile(Compass.S, this.building) && parentSouthTile.GetInfrastructureItem().IsConstructed(parentSouthTile.GetTileNumber(), true))
                {
                    // If the 2nd tile is to the west
                    Tile westTile = tile.GetNeighbour(Compass.W);
                    if (tile.SimilarItemNextToTile(Compass.W, this.building) && westTile.GetInfrastructureItem().IsConstructed(westTile.GetTileNumber(), true))
                    {
                        return buildingDynamicTexture.SpriteNameLSouthWest();
                    }
                }
            }

            // If there are three connections
            if (amountOfConnections == 3)
            {
                // If one of the tiles is to the north
                Tile grandParentNorthTile = tile.GetNeighbour(Compass.N);
                if (tile.SimilarItemNextToTile(Compass.N, this.building) && grandParentNorthTile.GetInfrastructureItem().IsConstructed(grandParentNorthTile.GetTileNumber(), true))
                {
                    // If one of the tiles is to the east
                    Tile parentEastTile = tile.GetNeighbour(Compass.E);
                    if (tile.SimilarItemNextToTile(Compass.E, this.building) && parentEastTile.GetInfrastructureItem().IsConstructed(parentEastTile.GetTileNumber(), true))
                    {
                        // If the 3rd tile is to the south
                        Tile southTile = tile.GetNeighbour(Compass.S);
                        if (tile.SimilarItemNextToTile(Compass.S, this.building) && southTile.GetInfrastructureItem().IsConstructed(southTile.GetTileNumber(), true))
                        {
                            return buildingDynamicTexture.SpriteNameTNorthEastSouth();
                        }

                        // If the 3rd tile is to the west
                        Tile westTile = tile.GetNeighbour(Compass.W);
                        if (tile.SimilarItemNextToTile(Compass.W, this.building) && westTile.GetInfrastructureItem().IsConstructed(westTile.GetTileNumber(), true))
                        {
                            return buildingDynamicTexture.SpriteNameTNorthEastWest();
                        }
                    }

                    // If one of the tiles is to the south
                    Tile parentSouthTile = tile.GetNeighbour(Compass.S);
                    if (tile.SimilarItemNextToTile(Compass.S, this.building) && parentSouthTile.GetInfrastructureItem().IsConstructed(parentSouthTile.GetTileNumber(), true))
                    {
                        // If the 3rd tile is to the west
                        Tile westTile = tile.GetNeighbour(Compass.W);
                        if (tile.SimilarItemNextToTile(Compass.W, this.building) && westTile.GetInfrastructureItem().IsConstructed(westTile.GetTileNumber(), true))
                        {
                            return buildingDynamicTexture.SpriteNameTNorthSouthWest();
                        }
                    }
                }


                //If one of the tiles is to the east
                Tile grandParentEastTile = tile.GetNeighbour(Compass.E);
                if (tile.SimilarItemNextToTile(Compass.E, this.building) && grandParentEastTile.GetInfrastructureItem().IsConstructed(grandParentEastTile.GetTileNumber(), true))
                {
                    // If one of the tiles is to the south
                    Tile parentSouthTile = tile.GetNeighbour(Compass.S);
                    if (tile.SimilarItemNextToTile(Compass.S, this.building) && parentSouthTile.GetInfrastructureItem().IsConstructed(parentSouthTile.GetTileNumber(), true))
                    {
                        // If one of the tiles is to the west
                        Tile westTile = tile.GetNeighbour(Compass.W);
                        if (tile.SimilarItemNextToTile(Compass.W, this.building) && westTile.GetInfrastructureItem().IsConstructed(westTile.GetTileNumber(), true))
                        {
                            return buildingDynamicTexture.SpriteNameTEastSouthWest();
                        }
                    }
                }
            }

            // If there are four connections
            if (amountOfConnections == 4)
            {
                return buildingDynamicTexture.SpriteNameCross();
            }

            return buildingDynamicTexture.SpriteNameHorizontal();
        }
    }
}
