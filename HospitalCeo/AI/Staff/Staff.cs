using System;
using Nez;
using Microsoft.Xna.Framework;
using HospitalCeo.AI.Interfaces;
using Nez.Textures;

/*
 * Staff Class
 */

namespace HospitalCeo.AI.Staff
{
    public abstract class Staff : Mob, IMobSwapSprite
    {
        protected MobSwapSpriteRenderer renderer;

        // IMobSwapSprite Interface
        public abstract Subtexture GetNorthFacingSprite();
        public abstract Subtexture GetEastFacingSprite();
        public abstract Subtexture GetSouthFacingSprite();
        public abstract Subtexture GetWestFacingSprite();
        public abstract bool UsesEastSprite();

        public Staff(Vector2 position) : base(position)
        {
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            AddRenderer();
        }

        public override string GetName()
        {
            return "Staff No-one";
        }

        private void AddRenderer()
        {
            renderer = new MobSwapSpriteRenderer(GetNorthFacingSprite());
            entity.addComponent(renderer);
        }
    }
}
