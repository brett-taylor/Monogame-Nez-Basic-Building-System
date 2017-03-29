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
        public HospitalCeo() : base(1000, 600)
        {
        }

        public void update()
        {
            World.WorldController.Update();
            InputManager.Update();
        }

        protected override void Initialize()
        {
            Core.debugRenderEnabled = false;
            Window.AllowUserResizing = true;
            Core.exitOnEscapeKeypress = false;
            base.Initialize();
            Core.registerGlobalManager(this);
            Transform.shouldRoundPosition = true;

            Utils.GlobalContent.Initialise();
            World.WorldController.Initialize();
            InputManager.Initialise();
            Building.BuildingController.Initialise();

            for (int i = 0; i < 20; i++)
                World.WorldController.SCENE.createEntity("worker" + i).addComponent<AI.Staff.Workman>(new AI.Staff.Workman(new Vector2((int) (400 + (i * 50)), (int) (400 + (i * 50)))));
        }
    }
}
