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
        public HospitalCeo() : base()
        {
        }

        public void update()
        {
            World.WorldController.Update();
            InputManager.Update();
            Building.InfrastructureBuildingController.Update();
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            Core.exitOnEscapeKeypress = false;
            base.Initialize();
            Core.registerGlobalManager(this);

            Utils.GlobalContent.Initialise();
            World.WorldController.Initialize();
            InputManager.Initialise();
        }
    }
}
