using Microsoft.Xna.Framework;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class FlooringCheapConcrete : Building
    {
        public FlooringCheapConcrete(Vector2 position) : base(position)
        {
        }

        public override string GetName()
        {
            return "Cheap Concrete Flooring";
        }

        public override Subtexture GetSprite()
        {
            return Utils.GlobalContent.Flooring.CheapConcrete;
        }

        public override BuildingType GetBuildingType()
        {
            return BuildingType.Infrastructure;
        }
    }
}
