using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.AI.Pathfinding;

/*
 * Brett Taylor
 * World Controller, handles creating the world
 * Referring to the world or tiles.
 */

namespace HospitalCeo.World
{
    public static class WorldController
    {
        public const int SCREEN_SPACE_RENDER_LAYER = 999;
        public static Scene SCENE { get; private set; }
        public const int WORLD_WIDTH = 100, WORLD_HEIGHT = 100;

        public static Tile[,] tiles { get; private set; }
        public static Entity inputManager { get; private set; }
        private static Entity tilesGraphic;
        public static Pathfinding.PathfindingHuman PATHFINDING_HUMAN_GRID { get; set; }

        public static void Initialize()
        {
            // Create the scene
            SCENE = new Scene();
            SCENE.clearColor = new Color(25, 25, 25);
            SCENE.addRenderer(new ScreenSpaceRenderer(100, SCREEN_SPACE_RENDER_LAYER));
            SCENE.addRenderer(new RenderLayerExcludeRenderer(0, SCREEN_SPACE_RENDER_LAYER));
            Core.scene = SCENE;

            // Create the world
            CreateBaseTiles();

            // Create the pathfind
            PATHFINDING_HUMAN_GRID = new Pathfinding.PathfindingHuman();
        }

        private static void CreateBaseTiles()
        {
            float[,] noiseMap = Utils.Noise.Calc2D(WORLD_WIDTH, WORLD_HEIGHT, 0.10f);
            tiles = new Tile[WORLD_WIDTH, WORLD_HEIGHT];

            for (int x = 0; x < tiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y < tiles.GetUpperBound(1); y++)
                {
                    Subtexture texture;
                    if (noiseMap[x, y] < 30f) texture = Utils.GlobalContent.Earth.Dirt;
                    else if (noiseMap[x, y] > 220f) texture = Utils.GlobalContent.Earth.Sand;
                    else texture = Utils.GlobalContent.Earth.Grass;

                    Tile tile = new Tile(texture, new Vector2(-50 + (x + 1) * 100, -50 + (y + 1) * 100), new Vector2(x, y));
                    tiles[x, y] = tile;
                }
            }

            tilesGraphic = SCENE.createEntity("Tiles graphic");
            tilesGraphic.addComponent<TileRenderer>(new TileRenderer());
        }

        public static Tile GetMouseOverTile(bool ShouldUseToFloor = false)
        {
            Tile t = GetTileAt(InputManager.GetScreenWorldPoint(), ShouldUseToFloor);
            return t != null ? t : null;
        }

        /*
         * ShouldUseToFloor should be used when dealing with negative tiles
         * as floor will return floor(-1.5) = 2.0
         * as (int) will return (int) -1.5 = -1
         */
        public static Tile GetTileAt(Vector2 coord, bool ShouldUseToFloor = false, bool isTileCoords = false)
        {
            int x = (int)coord.X;
            int y = (int)coord.Y;

            if (!isTileCoords)
            {
                if (ShouldUseToFloor)
                {
                    x = Mathf.floorToInt(x) / 100;
                    y = Mathf.floorToInt(y) / 100;
                }
                else
                {
                    x = x / 100;
                    y = y / 100;
                }
            }

            return GetTileAt(x, y);
        }

        public static Tile GetTileAt(int x, int y)
        {
            if (x >= WORLD_WIDTH || x < 0 || y >= WORLD_HEIGHT || y < 0)
            {
                return null;
            }

            return tiles[x, y] != null ? tiles[x, y] : null;
        }
    }
}
