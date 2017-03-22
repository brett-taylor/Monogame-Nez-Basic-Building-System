using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class FlooringCheapConcrete : BuildingLogic
    {
        public override string GetName()
        {
            return "Cheap Concrete Flooring";
        }

        public override BuildingType GetBuildingType()
        {
            return BuildingType.Infrastructure;
        }

        public override Subtexture GetTexture()
        {
            return Utils.GlobalContent.Flooring.CheapConcrete;
        }

        public override Vector2 GetTileSize()
        {
            return tileSize;
        }

        public override BuildingCategory GetBuildingCatergory()
        {
            return BuildingCategory.Flooring;
        }
    }
}
