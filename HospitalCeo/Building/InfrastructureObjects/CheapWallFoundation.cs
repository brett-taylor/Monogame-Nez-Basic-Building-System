using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class CheapWallFoundation : BuildingLogic, IBuildingBeforeBuild
    {
        public override string GetName()
        {
            return "Cheap Wall Foundation";
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

        public override bool CarryOnBuildingAfterBuild()
        {
            return false;
        }

        public override BuildingCategory GetBuildingCatergory()
        {
            return BuildingCategory.Foundation;
        }

        bool IBuildingBeforeBuild.ShouldActuallyBuild()
        {
            return false;
        }

        public void OnBeforeBuild(Vector2 tilePosition, Vector2 tileSize)
        {
            Entity foundation = World.WorldController.SCENE.createEntity("Foundation entity create");
            BuildingFoundationComponent foundationComponent = new BuildingFoundationComponent(typeof(CheapWall), typeof(FlooringCheapConcrete), tilePosition, tileSize);
            foundation.addComponent<BuildingFoundationComponent>(foundationComponent);
        }
    }
}
