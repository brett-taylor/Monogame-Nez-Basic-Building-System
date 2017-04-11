using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework;

namespace HospitalCeo
{
    public static class GameStateManager
    { 
        public static GameState GAME_STATE { get; set; }

        public static void Initialise(HospitalCeo hospitalCeo)
        {
            Core.debugRenderEnabled = false;
            hospitalCeo.Window.AllowUserResizing = true;
            Core.exitOnEscapeKeypress = false;
            Core.registerGlobalManager(hospitalCeo);
            Transform.shouldRoundPosition = true;
            Core.pauseOnFocusLost = false;

            GAME_STATE = GameState.StartMenu;
            Utils.GlobalContent.Initialise();
            UI.UIManager.Create("StartGameMainMenu", new UI.Elements.UI_World.StartGameMainMenu());
        }

        public static void Update()
        {
            if (GAME_STATE == GameState.Playing)
            {
                InputManager.Update();
                Pathfinding.PathfindManager.Update();
            }

            if (GAME_STATE == GameState.Exit)
            {
                Core.exit();
            }
        }

        public static void StartGame()
        {
            GAME_STATE = GameState.Playing;
            World.WorldController.Initialize();
            InputManager.Initialise();
            Building.BuildingController.Initialise();
            Pathfinding.PathfindManager.Initialise();
            Zoning.ZoneController.Initialise();
            UI.UIManager.Create("ConstructionMenu", new UI.Elements.UI_GAME.ConstructionMenu());

            int x = 400;
            int y = 400;
            for (int i = 1; i < 6; i++)
            {
                Entity e = World.WorldController.SCENE.createEntity("worker" + i);
                e.addComponent(new AI.Staff.Workman());
                e.addComponent(new Appearance_Builder.WorkerAppearence());
                e.position = new Vector2(x, y);

                x += 70;
                if (i % 3 == 0)
                {
                    y += 150;
                    x = 400;
                }
            }
        }
    }
}
