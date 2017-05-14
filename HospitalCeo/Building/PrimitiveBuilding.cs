using Microsoft.Xna.Framework;

namespace HospitalCeo.Building
{
    public abstract class PrimitiveBuilding
    {
        public abstract string GetName();
        public abstract Vector2 GetSize();
        public abstract BuildingType GetBuildingType();
        public abstract BuildingCategory GetBuildingCategory();

        public virtual int GetMovementCost()
        {
            return 0;
        }

        public virtual bool CarryOnBuildingAfterPlacement()
        {
            return true;
        }
    }
}
