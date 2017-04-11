using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class CheapWall : BuildingLogic, IBuildingWallSprites, IBuildingCustomRenderer
    {
        public override BuildingType GetBuildingType()
        {
            return BuildingType.Infrastructure;
        }

        public override string GetName()
        {
            return "Cheap Wall";
        }

        public override Subtexture GetTexture()
        {
            return SpriteNameHorizontal();
        }

        public override Vector2 GetTileSize()
        {
            return tileSize;
        }

        public override BuildingCategory GetBuildingCatergory()
        {
            return BuildingCategory.Wall;
        }

        void IBuildingCustomRenderer.DoCustomRenderer(BuildingBaseRenderer renderer)
        {
        }

        Type IBuildingCustomRenderer.GetRenderer()
        {
            return typeof(BuildingWallRenderer);
        }

        public override int GetMovementCost()
        {
            return 99;
        }

        // ---------- Start of the IBuildingWallSprites interface
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
            return Utils.GlobalContent.Wall.BaseWall.L_North_East;
        }

        public virtual Subtexture SpriteNameLNorthWest()
        {
            return Utils.GlobalContent.Wall.BaseWall.L_North_West;
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
