using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class Foundation : Building
    {
        public Foundation(Vector2 position) : base(position)
        {
        }

        public override string GetName()
        {
            return "Foundation";
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
