using Microsoft.Xna.Framework;
using Nez.Textures;

namespace HospitalCeo.Zoning
{
    public class Office : Zone
    {
        public Office(Vector2 position, Vector2 size) : base(position, size)
        {
        }

        protected override string GetName()
        {
            return "Office";
        }

        public override Color GetColor()
        {
            return Color.MediumPurple;
        }

        public override Subtexture GetTexture()
        {
            return Utils.GlobalContent.Zoning.Purple;
        }
    }
}
