using HospitalCeo.Building.Shared_Components;
using Nez.Textures;


namespace HospitalCeo.Building.Infrastructure.Components
{
    public class BuildingRepeatedTextureRenderer : BuildingRenderer
    {
        public BuildingRepeatedTextureRenderer(Building building, Subtexture texture) : base(BuildingType.Infrastructure, building, texture)
        {
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
        }
    }
}
