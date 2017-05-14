using HospitalCeo.Building.Infrastructure.Interfaces;
using Microsoft.Xna.Framework;
using Nez;
using HospitalCeo.Building.Infrastructure;

namespace HospitalCeo.Building.Infrastructure
{
    public class ConcreteWallFoundation : PrimitiveBuilding, IBuildingBeforeBuild
    {
        public void BeforeBuild(Building building)
        {
            BuildingController.PlaceBuilding(new ConcreteWall(), new Vector2(building.GetPosition().X, building.GetPosition().Y), new Vector2(building.GetTileSize().X, 1));
            BuildingController.PlaceBuilding(new ConcreteWall(), new Vector2(building.GetPosition().X, building.GetPosition().Y + building.GetTileSize().Y), new Vector2(building.GetTileSize().X, 1));
            BuildingController.PlaceBuilding(new ConcreteWall(), new Vector2(building.GetPosition().X, building.GetPosition().Y + 1), new Vector2(1, building.GetTileSize().Y - 2));
            BuildingController.PlaceBuilding(new ConcreteWall(), new Vector2(building.GetPosition().X + building.GetTileSize().X, building.GetPosition().Y + 1), new Vector2(1, building.GetTileSize().Y - 2));
        }

        public bool ContinueBuild()
        {
            return false;
        }

        public override BuildingCategory GetBuildingCategory()
        {
            return BuildingCategory.WALL;
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
            return "Concrete Wall Foundation";
        }

        public override Vector2 GetSize()
        {
            return new Vector2(1, 1);
        }
    }
}
