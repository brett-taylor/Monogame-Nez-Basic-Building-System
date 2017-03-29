using Nez;
using Nez.Textures;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using HospitalCeo.AI.Staff;

namespace HospitalCeo.AI
{
    public class MobSwapSpriteRenderer : Sprite, IUpdatable
    {
        private Vector2 lastPosition;
        private Staff.Staff staff;

        public MobSwapSpriteRenderer(Subtexture tex) : base(tex)
        {
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            lastPosition = entity.position;
            renderLayer = 15;
            staff = entity.getComponent<Staff.Staff>();
        }

        public void update()
        {
            Vector2 offsetPosition = entity.position - lastPosition;

            // Heading North
            if (offsetPosition.Y < -1)
            {
                setSubtexture(staff.GetNorthFacingSprite());
            }

            // Heading South
            else if (offsetPosition.Y > 1)
            {
                setSubtexture(staff.GetSouthFacingSprite());
            }

            // Heading West
            else if (offsetPosition.X < -1)
            {
                setSubtexture(staff.GetWestFacingSprite());
                if (!staff.UsesEastSprite())
                {
                    flipX = false;
                }
            }

            // Heading East
            else if (offsetPosition.X > 1)
            {
                if (staff.UsesEastSprite())
                    setSubtexture(staff.GetEastFacingSprite());
                else
                {
                    setSubtexture(staff.GetWestFacingSprite());
                    flipX = true;
                }
            }

            lastPosition = entity.position;
        }
    }
}
