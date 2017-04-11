using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

/*
 * Brett Taylor
 * Main Game Class
 */

namespace HospitalCeo
{
    public class HospitalCeo : Core, IUpdatableManager
    {
        public HospitalCeo() : base(width: 1000, height: 600, windowTitle: "Some Building Game")
        {
        }

        public void update()
        {
            GameStateManager.Update();
        }

        protected override void Initialize()
        {
            base.Initialize();
            GameStateManager.Initialise(this);
        }
    }
}
