using Microsoft.Xna.Framework;

namespace HospitalCeo.Building
{
    interface IBuildingBeforeBuild
    {
        bool ShouldActuallyBuild();
        void OnBeforeBuild(Vector2 tilePosition, Vector2 tileSize);
    }
}
