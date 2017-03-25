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
            Tasks.TaskManager.Update();
        }

        protected override void Initialize()
        {
            Core.debugRenderEnabled = false;
            Window.AllowUserResizing = true;
            Core.exitOnEscapeKeypress = false;
            base.Initialize();
            Core.registerGlobalManager(this);

            Utils.GlobalContent.Initialise();
            World.WorldController.Initialize();
            InputManager.Initialise();
            Building.BuildingController.Initialise();
            Tasks.TaskManager.Initialise();

            for (int i = 0; i < 1000; i++)
            {
                World.WorldController.SCENE.createEntity("worker " + i).addComponent<AI.Staff.Workman>(new AI.Staff.Workman(new Vector2(400 + (i * 1), 400 + (i * 1))));
            }

            Entity entity = World.WorldController.SCENE.createEntity("worker");
            AI.Staff.Workman worker = new AI.Staff.Workman(new Vector2(550, 550));
            entity.addComponent(worker);

        }
    }
}
