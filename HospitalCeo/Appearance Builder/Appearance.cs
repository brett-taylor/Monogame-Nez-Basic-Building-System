using System;
using System.Collections.Generic;
using Nez;
using Microsoft.Xna.Framework;
using Nez.Textures;

/*
 * Brett Taylor
 * Appearance class.
 * Holds the hair, head, body
 * Handles rotating the character etc
 */
namespace HospitalCeo.Appearance_Builder
{
    public abstract class Appearance : Component
    {
        protected Bodypart hair;
        protected Bodypart head;
        protected Bodypart body;
        protected abstract Subtexture[] GetBodyTextures();
        protected abstract Subtexture[] GetHeadTextures();
        protected abstract Subtexture[] GetHairTextures();
        protected abstract Color[] GetPossibleHairColors();
        protected abstract Color[] GetPossibleHeadColors();
        protected abstract Color[] GetPossibleBodyColors();

        public Appearance()
        {
            body = new Bodypart(this, Color.Pink, Utils.GlobalContent.Character.WorkerBody.BodyOne.North, Utils.GlobalContent.Character.WorkerBody.BodyOne.South, Utils.GlobalContent.Character.WorkerBody.BodyOne.North, 15, 15, 15);
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            body.Show();
        }
    }
}
