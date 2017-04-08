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
        public HospitalCeo() : base(width: 1000, height: 600, windowTitle: "Hospital CEO")
        {
        }

        public void update()
        {
            InputManager.Update();
            Pathfinding.PathfindManager.Update();
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
            Pathfinding.PathfindManager.Initialise();

            for (int i = 0; i < 70; i++)
                World.WorldController.SCENE.createEntity("worker" + i).addComponent<AI.Staff.Workman>(new AI.Staff.Workman(new Vector2((int) (400 + (i * 50)), (int) (400 + (i * 50)))));
        }
    }
}
