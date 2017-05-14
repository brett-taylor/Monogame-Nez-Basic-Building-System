using Microsoft.Xna.Framework;
using Nez;
using System;

namespace HospitalCeo
{
    public static class GameStateManager
    { 
        public static GameState GAME_STATE { get; set; }
        public static float LOAD_PERCENT { get; private set; }

        public static void Initialise(HospitalCeo hospitalCeo)
        {
            Core.debugRenderEnabled = false;
            hospitalCeo.Window.AllowUserResizing = true;
            Core.exitOnEscapeKeypress = false;
            Core.registerGlobalManager(hospitalCeo);
            Transform.shouldRoundPosition = true;
            Core.pauseOnFocusLost = false;

            GAME_STATE = GameState.StartMenu;
            UI.UIManager.Create("StartGameMainMenu", new UI.Elements.UI_World.StartGameMainMenu());
        }

        public static void Update()
        {
            UI.UIManager.Update();

            if (GAME_STATE == GameState.Playing)
            {
                InputManager.Update();
                Pathfinding.PathfindManager.Update();
            }

            if (GAME_STATE == GameState.Exit)
                Core.exit();

            if (GAME_STATE == GameState.Loading)
            {
                ShowGame();
            }
        }

        public static void StartGame()
        {
            GAME_STATE = GameState.Loading;
            UI.UIManager.Destory("StartGameMainMenu");
        }

        private static void ShowGame()
        {
            GAME_STATE = GameState.Playing;
            Utils.GlobalContent.Initialise();
            World.WorldController.Initialize();
            InputManager.Initialise();
            Building.BuildingController.Initialise();
            Pathfinding.PathfindManager.Initialise();
            Zoning.ZoneController.Initialise();
            Lua.LuaManager.LoadAllFilesInLuaDirectory();
            UI.UIManager.Create("ConstructionMenu", new UI.Elements.UI_GAME.ConstructionMenu());

            /*Entity northWall = World.WorldController.SCENE.createEntity("Building at x: 8 y: 10");
            northWallBuilding = new Building.Building();
            northWall.addComponent(northWallBuilding);
            northWallBuilding.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            northWallBuilding.SetInfo(new Vector2(8, 10), new Vector2(18, 1));

            Entity eastWall = World.WorldController.SCENE.createEntity("Building at x: 16 y: 10");
            eastWallBuilding = new Building.Building();
            eastWall.addComponent(eastWallBuilding);
            eastWallBuilding.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            eastWallBuilding.SetInfo(new Vector2(26, 10), new Vector2(1, 10));

            Entity southhWall = World.WorldController.SCENE.createEntity("Building at x: 8 y: 14");
            southWallBuilding = new Building.Building();
            southhWall.addComponent(southWallBuilding);
            southWallBuilding.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            southWallBuilding.SetInfo(new Vector2(8, 19), new Vector2(18, 1));

            Entity westNorthWall = World.WorldController.SCENE.createEntity("Building at x: 8 y: 11");
            westNorthBuilding = new Building.Building();
            westNorthWall.addComponent(westNorthBuilding);
            westNorthBuilding.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            westNorthBuilding.SetInfo(new Vector2(8, 11), new Vector2(1, 4));

            Entity southEastWall = World.WorldController.SCENE.createEntity("Building at x: 8 y: 16");
            southEastBuilding = new Building.Building();
            southEastWall.addComponent(southEastBuilding);
            southEastBuilding.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            southEastBuilding.SetInfo(new Vector2(8, 16), new Vector2(1, 3));

            Entity wallOneEntity = World.WorldController.SCENE.createEntity("Building");
            wallOne = new Building.Building();
            wallOneEntity.addComponent(wallOne);
            wallOne.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            wallOne.SetInfo(new Vector2(17, 11), new Vector2(1, 4));

            Entity wallTwoEntity = World.WorldController.SCENE.createEntity("Building");
            wallTwo = new Building.Building();
            wallTwoEntity.addComponent(wallTwo);
            wallTwo.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            wallTwo.SetInfo(new Vector2(19, 14), new Vector2(7, 1));

            Entity wallThreeEntity = World.WorldController.SCENE.createEntity("Building");
            wallThree = new Building.Building();
            wallThreeEntity.addComponent(wallThree);
            wallThree.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            wallThree.SetInfo(new Vector2(9, 16), new Vector2(7, 1));

            Entity wallFourEntity = World.WorldController.SCENE.createEntity("Building");
            wallFour = new Building.Building();
            wallFourEntity.addComponent(wallFour);
            wallFour.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteWall());
            wallFour.SetInfo(new Vector2(17, 16), new Vector2(1, 3));

            Entity floorOneBuilding = World.WorldController.SCENE.createEntity("Building");
            Entity floorTwoBuilding = World.WorldController.SCENE.createEntity("Building");
            Entity floorThreeBuilding = World.WorldController.SCENE.createEntity("Building");
            Entity floorFourBuilding = World.WorldController.SCENE.createEntity("Building");
            Entity floorFiveBuilding = World.WorldController.SCENE.createEntity("Building");
            Entity floorSixBuilding = World.WorldController.SCENE.createEntity("Building");
            Entity floorSevenBuilding = World.WorldController.SCENE.createEntity("Building");
            Entity floorEightBuilding = World.WorldController.SCENE.createEntity("Building");
            floorOne = new Building.Building();
            floorTwo = new Building.Building();
            floorThree = new Building.Building();
            floorFour = new Building.Building();
            floorFive = new Building.Building();
            floorSix = new Building.Building();
            floorSeven = new Building.Building();
            floorEight = new Building.Building();
            floorOneBuilding.addComponent(floorOne);
            floorTwoBuilding.addComponent(floorTwo);
            floorThreeBuilding.addComponent(floorThree);
            floorFourBuilding.addComponent(floorFour);
            floorFiveBuilding.addComponent(floorFive);
            floorSixBuilding.addComponent(floorSix);
            floorSevenBuilding.addComponent(floorSeven);
            floorEightBuilding.addComponent(floorEight);
            floorOne.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorTwo.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorThree.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorFour.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorFive.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorSix.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorSeven.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorEight.SetPrimitiveBuilding(new Building.Infrastructure.ConcreteFloorOne());
            floorOne.SetInfo(new Vector2(9, 11), new Vector2(8, 5));
            floorTwo.SetInfo(new Vector2(18, 11), new Vector2(8, 3));
            floorThree.SetInfo(new Vector2(18, 15), new Vector2(8, 4));
            floorFour.SetInfo(new Vector2(9, 17), new Vector2(8, 2));
            floorFive.SetInfo(new Vector2(8, 15), new Vector2(1, 1));
            floorSix.SetInfo(new Vector2(16, 16), new Vector2(1, 1));
            floorSeven.SetInfo(new Vector2(17, 15), new Vector2(1, 1));
            floorEight.SetInfo(new Vector2(18, 14), new Vector2(1, 1));

            Core.schedule(1f, action => {
                northWallBuilding.Finish();
                eastWallBuilding.Finish();
                southWallBuilding.Finish();
                westNorthBuilding.Finish();
                southEastBuilding.Finish();
                wallOne.Finish();
                wallTwo.Finish();
                wallThree.Finish();
                wallFour.Finish();
                floorOne.Finish();
                floorTwo.Finish();
                floorThree.Finish();
                floorFour.Finish();
                floorFive.Finish();
                floorSix.Finish();
                floorSeven.Finish();
                floorEight.Finish();
                Tasks.Task.Clear();
            });

            Core.schedule(1f, action => {
                Entity e1 = World.WorldController.SCENE.createEntity("Worker");
                workmanOne = e1.addComponent(new AI.Staff.Workman());
                e1.addComponent(new Appearance.WorkerAppearence());
                e1.position = new Vector2(1150, 1250);
                workmanOne.SetCurrentInstruction(TestPathfindingWorkmanCreateInstruction());

                Entity e2 = World.WorldController.SCENE.createEntity("Worker");
                workmanTwo = e2.addComponent(new AI.Staff.Workman());
                e2.addComponent(new Appearance.WorkerAppearence());
                e2.position = new Vector2(1950, 1350);
                workmanTwo.SetCurrentInstruction(TestPathfindingWorkmanCreateInstruction());

                Entity e3 = World.WorldController.SCENE.createEntity("Worker");
                workmanThree = e3.addComponent(new AI.Staff.Workman());
                e3.addComponent(new Appearance.WorkerAppearence());
                e3.position = new Vector2(2250, 1700);
                workmanThree.SetCurrentInstruction(TestPathfindingWorkmanCreateInstruction());

                Entity e4 = World.WorldController.SCENE.createEntity("Worker");
                workmanFour = e4.addComponent(new AI.Staff.Workman());
                e4.addComponent(new Appearance.WorkerAppearence());
                e4.position = new Vector2(650, 1550);
                workmanFour.SetCurrentInstruction(TestPathfindingWorkmanCreateInstruction());

                workmanOne.SetCanTakeJobs(false);
                workmanTwo.SetCanTakeJobs(false);
                workmanThree.SetCanTakeJobs(false);
                workmanFour.SetCanTakeJobs(false);
            });
        */}/*

        private static Building.Building northWallBuilding, eastWallBuilding, southWallBuilding, westNorthBuilding, southEastBuilding, wallOne, wallTwo, wallThree, wallFour, floorOne, floorTwo, floorThree, floorFour, floorFive, floorSix, floorSeven, floorEight;
        private static AI.Staff.Workman workmanOne, workmanTwo, workmanThree, workmanFour;

        private static World.Tile TestPathfindingWorkmanGetTile()
        {
            World.Tile tile;
            int area = Nez.Random.range(1, 6);
            Vector2 topLeftPos, bottomRightPos;
            switch (area)
            {
                case 1:
                    topLeftPos = new Vector2(9, 11);
                    bottomRightPos = new Vector2(14, 14);
                    break;
                case 2:
                    topLeftPos = new Vector2(17, 13);
                    bottomRightPos = new Vector2(25, 14);
                    break;
                case 3:
                    topLeftPos = new Vector2(18, 15);
                    bottomRightPos = new Vector2(25, 18);
                    break;
                case 4:
                    topLeftPos = new Vector2(9, 17);
                    bottomRightPos = new Vector2(16, 18);
                    break;
                case 5:
                default:
                    topLeftPos = new Vector2(5, 14);
                    bottomRightPos = new Vector2(7, 16);
                    break;
            }

            tile = World.WorldController.GetTileAt(Nez.Random.range((int) topLeftPos.X, (int) bottomRightPos.X), Nez.Random.range((int) topLeftPos.Y, (int) bottomRightPos.Y));
            if (tile == null)
                tile = World.WorldController.GetTileAt(9, 17);

            return tile;
        }

        private static Tasks.Instruction TestPathfindingWorkmanCreateInstruction()
        {
            Tasks.Instruction instruction = new Tasks.Instruction();
            Tasks.Process process = new Tasks.Process(TestPathfindingWorkmanGetTile(), 1f, onreach => {
                onreach.GetInstruction().GetEntity().getComponent<AI.Staff.Workman>().SetCurrentInstruction(TestPathfindingWorkmanCreateInstruction());
            });
            instruction.AddProcess(process);

            return instruction;
        }*/
    }
}
