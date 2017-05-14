using System;
using HospitalCeo.Building.Gameplay.Interfaces;
using HospitalCeo.Building.Shared_Interfaces;
using Microsoft.Xna.Framework;
using Nez.Textures;

namespace HospitalCeo.Building.Gameplay
{
    public class OfficeTable : PrimitiveBuilding, IBuildingStandardGameplayRenderer, IBuildingRequiresConstruction
    {
        public override BuildingCategory GetBuildingCategory()
        {
            return BuildingCategory.OBJECT;
        }

        public override BuildingType GetBuildingType()
        {
            return BuildingType.Gameplay;
        }

        public override string GetName()
        {
            return "Office Desk";
        }

        public override Vector2 GetSize()
        {
            return new Vector2(2, 1);
        }

        public Subtexture GetTexture()
        {
            return Utils.GlobalContent.Objects.OfficeTable.North_South;
        }

        public float GetTimeRequiredToBuild()
        {
            return 5f;
        }

        public override bool CarryOnBuildingAfterPlacement()
        {
            return true;
        }
    }
}
