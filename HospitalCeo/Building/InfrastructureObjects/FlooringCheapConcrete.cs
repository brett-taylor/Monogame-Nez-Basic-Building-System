using Microsoft.Xna.Framework;

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

        public override string GetSpriteName()
        {
            return "hospitalceo/Tiles/Tile_CheapConcrete";
        }

        public override BuildingType GetBuildingType()
        {
            return BuildingType.Infrastructure;
        }
    }
}
