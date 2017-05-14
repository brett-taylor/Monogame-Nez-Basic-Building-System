using System;
using HospitalCeo.Building.Infrastructure.Interfaces;
using HospitalCeo.Building.Shared_Interfaces;
using Microsoft.Xna.Framework;
using Nez.Textures;

namespace HospitalCeo.Building.Infrastructure
{
    public class CarpetFloorOne : PrimitiveBuilding, IBuildingRepeatedTextureRenderer, IBuildingRequiresConstruction
    {
        public override BuildingCategory GetBuildingCategory()
        {
            return BuildingCategory.FLOOR;
        }

        public override BuildingType GetBuildingType()
        {
            return BuildingType.Infrastructure;
        }

        public override int GetMovementCost()
        {
            return 0;
        }

        public override string GetName()
        {
            return "Carpet Floor One";
        }

        public override Vector2 GetSize()
        {
            return Vector2.One;
        }

        public Subtexture GetTexture()
        {
            return Utils.GlobalContent.Flooring.CheapCarpet;
        }

        public float GetTimeRequiredToBuild()
        {
            return 2f;
        }
    }
}
