using System;
using System.Collections.Generic;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class BuildingWallRenderer : BuildingBaseRenderer
    {
        public BuildingWallRenderer(BuildingLogic building) : base(building)
        {
            this.building = building;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            // Start from the top left of the building and go towards the bottom right correcting sprites
            // Once done run one loop outside of the building checking for walls connecting to it.
            SetInternalSprites();
            SetExternalSprites();
        }

        private void SetInternalSprites()
        {
            for (int x = 0; x < building.GetTileSize().X; x++)
            {
                for (int y = 0; y < building.GetTileSize().Y; y++)
                {
                    Tile t = WorldController.GetTileAt((int) building.GetTilePosition().X + x, (int) building.GetTilePosition().Y + y);
                    int amount = t.SimilarItemsAroundTile(this.building);

                    BuildingSprite sprite = new BuildingSprite(WorkOutSprite(amount, t), building.GetTilePosition() * 100 + new Vector2(x * 100, y * 100));
                    sprite.renderLayer = 18;
                    sprite.localOffset = new Vector2(x * 100, y * 100);
                    entity.addComponent<Sprite>(sprite);
                    sprites[x, y] = sprite;
                }
            }
        }

        private void SetExternalSprites()
        {
            for (int x = -1; x < building.GetTileSize().X + 1; x++)
            {
                // Tiles that are above the building
                SetOtherSprite(WorldController.GetTileAt((int) building.GetTilePosition().X + x, (int) building.GetTilePosition().Y - 1));

                // Tiles that are below the building
                SetOtherSprite(WorldController.GetTileAt((int) building.GetTilePosition().X + x, (int) building.GetTilePosition().Y + (int) building.GetTileSize().Y));
            }

            for (int y = -1; y < building.GetTileSize().Y + 1; y++)
            {
                // Tiles that are to the right
                SetOtherSprite(WorldController.GetTileAt((int)building.GetTilePosition().X -1, (int)building.GetTilePosition().Y + y));

                // Tiles that are to the left
                SetOtherSprite(WorldController.GetTileAt((int) building.GetTilePosition().X + (int) building.GetTileSize().X, (int)building.GetTilePosition().Y + y));
            }
        }

        private void SetOtherSprite(Tile tile)
        {
            if (tile != null)
            {
                BuildingLogic other = tile.GetInfrastructureItem();
                if (other != null)
                {
                    if (other.GetBuildingCatergory() == building.GetBuildingCatergory())
                    {
                        int amount = tile.SimilarItemsAroundTile(this.building);
                        BuildingWallRenderer otherWallRenderer = other.getComponent<BuildingWallRenderer>();
                        if (otherWallRenderer != null)
                        {
                            BuildingSprite sprite = otherWallRenderer.GetSpriteAt(tile.GetTileNumber());
                            if (sprite != null)
                                sprite.setSubtexture(WorkOutSprite(amount, tile));
                        }
                    }
                }
            }
        }

        private Subtexture WorkOutSprite(int amountOfConnections, Tile tile)
        {
            IBuildingWallSprites wall = building as IBuildingWallSprites;
            if (wall == null)
            {
                return Utils.GlobalContent.Util.White_Tile;
            }

            if (amountOfConnections == 0)
            {
                return wall.SpriteNameHorizontal();
            }

            // If there is one wall around it
            if (amountOfConnections == 1)
            {
                // If the tile is to the north
                if (tile.SimilarItemNextToTile(Compass.N, this.building))
                {
                    return wall.SpriteNameSouthEnding();
                }

                // If the tile is to the east
                if (tile.SimilarItemNextToTile(Compass.E, this.building))
                {
                    return wall.SpriteNameWestEnding();
                }

                // If the tile is to the south
                if (tile.SimilarItemNextToTile(Compass.S, this.building))
                {
                    return wall.SpriteNameNorthEnding();
                }

                // If the tile is to the west
                if (tile.SimilarItemNextToTile(Compass.W, this.building))
                {
                    return wall.SpriteNameEastEnding();
                }
            }

            // If there are two connections
            if (amountOfConnections == 2)
            {
                // If one of the tiles is north
                if (tile.SimilarItemNextToTile(Compass.N, this.building))
                {
                    //If the 2nd tile is to the east
                    if (tile.SimilarItemNextToTile(Compass.E, this.building))
                    {
                        return wall.SpriteNameLNorthEast();
                    }

                    //If the 2nd tile is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this.building))
                    {
                        return wall.SpriteNameVertical();
                    }

                    //If the 2nd tile is to the west
                    if (tile.SimilarItemNextToTile(Compass.W, this.building))
                    {
                        return wall.SpriteNameLNorthWest();
                    }
                }

                // If one of the tiles is to the east
                if (tile.SimilarItemNextToTile(Compass.E, this.building))
                {
                    // If the 2nd tile is to the west
                    if (tile.SimilarItemNextToTile(Compass.W, this.building))
                    {
                        return wall.SpriteNameHorizontal();
                    }

                    // If the 2nd tile is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this.building))
                    {
                        return wall.SpriteNameLEastSouth();
                    }
                }

                // If one of the tiles is to the south
                if (tile.SimilarItemNextToTile(Compass.S, this.building))
                {
                    // If the 2nd tile is to the west
                    if (tile.SimilarItemNextToTile(Compass.W, this.building))
                    {
                        return wall.SpriteNameLSouthWest();
                    }
                }
            }

            // If there are three connections
            if (amountOfConnections == 3)
            {
                // If one of the tiles is to the north
                if (tile.SimilarItemNextToTile(Compass.N, this.building))
                {
                    // If one of the tiles is to the east
                    if (tile.SimilarItemNextToTile(Compass.E, this.building))
                    {
                        // If the 3rd tile is to the south
                        if (tile.SimilarItemNextToTile(Compass.S, this.building))
                        {
                            return wall.SpriteNameTNorthEastSouth();
                        }

                        // If the 3rd tile is to the west
                        if (tile.SimilarItemNextToTile(Compass.W, this.building))
                        {
                            return wall.SpriteNameTNorthEastWest();
                        }
                    }

                    // If one of the tiles is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this.building))
                    {
                        // If the 3rd tile is to the west
                        if (tile.SimilarItemNextToTile(Compass.W, this.building))
                        {
                            return wall.SpriteNameTNorthSouthWest();
                        }
                    }
                }


                //If one of the tiles is to the east
                if (tile.SimilarItemNextToTile(Compass.E, this.building))
                {
                    // If one of the tiles is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this.building))
                    {
                        // If one of the tiles is to the west
                        if (tile.SimilarItemNextToTile(Compass.W, this.building))
                        {
                            return wall.SpriteNameTEastSouthWest();
                        }
                    }
                }
            }

            // If there are four connections
            if (amountOfConnections == 4)
            {
                return wall.SpriteNameCross();
            }

            return Utils.GlobalContent.Util.White_Tile;
        }
    }
}
