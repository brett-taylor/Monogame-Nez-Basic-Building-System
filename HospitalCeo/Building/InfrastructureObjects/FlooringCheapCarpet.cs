using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class FlooringCheapCarpet : BuildingLogic
    {
        public override BuildingType GetBuildingType()
        {
            return BuildingType.Infrastructure;
        }

        public override string GetName()
        {
            return "Cheap Carpet";
        }

        public override Subtexture GetTexture()
        {
            return Utils.GlobalContent.Flooring.CheapCarpet;
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
