using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class BaseWall : Building
    {
        protected bool shouldUpdateSprite = true;
        protected float spriteChangeCooldown = 0;

        public BaseWall(Vector2 position) : base(position)
        {
        }

        public override string GetName()
        {
            return "Basic Wall";
        }

        public override Subtexture GetSprite()
        {
            return SpriteNameHorizontal();
        }

        public override BuildingType GetBuildingType()
        {
            return BuildingType.Infrastructure;
        }

        public override bool CustomSpriteSettings()
        {
            return true;
        }

        public override bool OneSquareWidth()
        {
            return true;
        }

        protected override void Update()
        {
            // If spriteChangeCooldown is greater than 0 then minus the time
            // Walls tell other walls near it to update - this is used as a flag to tell its been updated so there is not a continous loop of neighbour tiles telling each other to update
            if (spriteChangeCooldown > 0)
                spriteChangeCooldown -= Time.deltaTime;

            if (spriteChangeCooldown < 0)
            {
                spriteChangeCooldown = 0f;
                shouldUpdateSprite = true;
            }
        }

        public override void SetSpritesCorrectly()
        {
            int amountOfConnections = tile.SimilarItemsAroundTile(this);
            if (amountOfConnections == 0) return;
            if (shouldUpdateSprite == false) return;

            // Stops neighbors constantly calling each other causing a continous loop and a stack overflow.
            if (amountOfConnections >= 1)
            {
                shouldUpdateSprite = false;
                spriteChangeCooldown = 0.2f;
            }

            // If there is one wall around it
            if (amountOfConnections == 1)
            {
                // If the tile is to the north
                if (tile.SimilarItemNextToTile(Compass.N, this))
                {
                    SetSprite(SpriteNameSouthEnding());
                    tile.GetNeighbour(Compass.N).GetInfrastructureItem().SetSpritesCorrectly();
                }

                // If the tile is to the east
                if (tile.SimilarItemNextToTile(Compass.E, this))
                {
                    SetSprite(SpriteNameWestEnding());
                    tile.GetNeighbour(Compass.E).GetInfrastructureItem().SetSpritesCorrectly();
                }

                // If the tile is to the south
                if (tile.SimilarItemNextToTile(Compass.S, this))
                {
                    SetSprite(SpriteNameNorthEnding());
                    tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                }

                // If the tile is to the west
                if (tile.SimilarItemNextToTile(Compass.W, this))
                {
                    SetSprite(SpriteNameEastEnding());
                    tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
                }
            }

            // If there are two connections
            if (amountOfConnections == 2)
            {
                // If one of the tiles is north
                if (tile.SimilarItemNextToTile(Compass.N, this))
                {
                    //If the 2nd tile is to the east
                    if (tile.SimilarItemNextToTile(Compass.E, this))
                    {
                        SetSprite(SpriteNameLNorthEast());
                        tile.GetNeighbour(Compass.E).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    //If the 2nd tile is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this))
                    {
                        SetSprite(SpriteNameVertical());
                        tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    //If the 2nd tile is to the west
                    if (tile.SimilarItemNextToTile(Compass.W, this))
                    {
                        SetSprite(SpriteNameLNorthWest());
                        tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    tile.GetNeighbour(Compass.N).GetInfrastructureItem().SetSpritesCorrectly();
                }

                // If one of the tiles is to the east
                if (tile.SimilarItemNextToTile(Compass.E, this))
                {
                    // If the 2nd tile is to the west
                    if (tile.SimilarItemNextToTile(Compass.W, this))
                    {
                        SetSprite(SpriteNameHorizontal());
                        tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    // If the 2nd tile is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this))
                    {
                        SetSprite(SpriteNameLEastSouth());
                        tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    tile.GetNeighbour(Compass.E).GetInfrastructureItem().SetSpritesCorrectly();
                }

                // If one of the tiles is to the south
                if (tile.SimilarItemNextToTile(Compass.S, this))
                {
                    // If the 2nd tile is to the west
                    if (tile.SimilarItemNextToTile(Compass.W, this))
                    {
                        SetSprite(SpriteNameLSouthWest());
                        tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                }
            }

            // If there are three connections
            if (amountOfConnections == 3)
            {
                // If one of the tiles is to the north
                if (tile.SimilarItemNextToTile(Compass.N, this))
                {
                    // If one of the tiles is to the east
                    if (tile.SimilarItemNextToTile(Compass.E, this))
                    {
                        // If the 3rd tile is to the south
                        if (tile.SimilarItemNextToTile(Compass.S, this))
                        {
                            SetSprite(SpriteNameTNorthEastSouth());
                            tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                        }

                        // If the 3rd tile is to the west
                        if (tile.SimilarItemNextToTile(Compass.W, this))
                        {
                            SetSprite(SpriteNameTNorthEastWest());
                            tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
                        }

                        tile.GetNeighbour(Compass.E).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    // If one of the tiles is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this))
                    {
                        // If the 3rd tile is to the west
                        if (tile.SimilarItemNextToTile(Compass.W, this))
                        {
                            SetSprite(SpriteNameTNorthSouthWest());
                            tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
                        }

                        tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    tile.GetNeighbour(Compass.N).GetInfrastructureItem().SetSpritesCorrectly();
                }


                //If one of the tiles is to the east
                if (tile.SimilarItemNextToTile(Compass.E, this))
                {
                    // If one of the tiles is to the south
                    if (tile.SimilarItemNextToTile(Compass.S, this))
                    {
                        // If one of the tiles is to the west
                        if (tile.SimilarItemNextToTile(Compass.W, this))
                        {
                            SetSprite(SpriteNameTEastSouthWest());
                            tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
                        }

                        tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                    }

                    tile.GetNeighbour(Compass.E).GetInfrastructureItem().SetSpritesCorrectly();
                }
            }

            // If there are four connections
            if (amountOfConnections == 4)
            {
                SetSprite(SpriteNameCross());
                tile.GetNeighbour(Compass.N).GetInfrastructureItem().SetSpritesCorrectly();
                tile.GetNeighbour(Compass.E).GetInfrastructureItem().SetSpritesCorrectly();
                tile.GetNeighbour(Compass.S).GetInfrastructureItem().SetSpritesCorrectly();
                tile.GetNeighbour(Compass.W).GetInfrastructureItem().SetSpritesCorrectly();
            }
        }

        public virtual Subtexture SpriteNameHorizontal()
        {
            return Utils.GlobalContent.Wall.BaseWall.Horizontal;
        }

        public virtual Subtexture SpriteNameVertical()
        {
            return Utils.GlobalContent.Wall.BaseWall.Vertical;
        }

        public virtual Subtexture SpriteNameWestEnding()
        {
            return Utils.GlobalContent.Wall.BaseWall.West_Ending;
        }

        public virtual Subtexture SpriteNameEastEnding()
        {
            return Utils.GlobalContent.Wall.BaseWall.East_Ending;
        }

        public virtual Subtexture SpriteNameNorthEnding()
        {
            return Utils.GlobalContent.Wall.BaseWall.North_Ending;
        }

        public virtual Subtexture SpriteNameSouthEnding()
        {
            return Utils.GlobalContent.Wall.BaseWall.South_Ending;
        }

        public virtual Subtexture SpriteNameLNorthEast()
        {
            return Utils.GlobalContent.Wall.BaseWall.L_Nort_East;
        }

        public virtual Subtexture SpriteNameLNorthWest()
        {
            return Utils.GlobalContent.Wall.BaseWall.L_Nort_West;
        }

        public virtual Subtexture SpriteNameLEastSouth()
        {
            return Utils.GlobalContent.Wall.BaseWall.L_East_South;
        }

        public virtual Subtexture SpriteNameLSouthWest()
        {
            return Utils.GlobalContent.Wall.BaseWall.L_South_West;
        }

        public virtual Subtexture SpriteNameTNorthEastSouth()
        {
            return Utils.GlobalContent.Wall.BaseWall.T_North_East_South;
        }

        public virtual Subtexture SpriteNameTNorthEastWest()
        {
            return Utils.GlobalContent.Wall.BaseWall.T_North_East_West;
        }

        public virtual Subtexture SpriteNameTNorthSouthWest()
        {
            return Utils.GlobalContent.Wall.BaseWall.T_North_South_West;
        }

        public virtual Subtexture SpriteNameTEastSouthWest()
        {
            return Utils.GlobalContent.Wall.BaseWall.T_East_South_West;
        }

        public virtual Subtexture SpriteNameCross()
        {
            return Utils.GlobalContent.Wall.BaseWall.Cross;
        }
    }
}
