using HospitalCeo.Building.Shared_Components;
using Nez.Textures;

namespace HospitalCeo.Building.Gameplay.Components
{
    public class BuildingStandardGameplayRenderer : BuildingRenderer
    {
        public BuildingStandardGameplayRenderer(Building building, Subtexture texture) : base(BuildingType.Gameplay, building, texture)
        {
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
        }
    }
}
