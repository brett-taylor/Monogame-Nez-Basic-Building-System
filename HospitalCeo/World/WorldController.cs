using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
/*
 * Brett Taylor
 * World Controller, handles creating the world
 * Referring to the world or tiles.
 */

namespace HospitalCeo.World
{
    public static class WorldController
    {
        public static Scene SCENE { get; private set; }
        public const int WORLD_WIDTH = 30, WORLD_HEIGHT = 30;
        public static Tile[,] tiles { get; private set; }
        public static Entity inputManager { get; private set; }

        public static void Initialize()
        {
            // Create the scene
            SCENE = Scene.createWithDefaultRenderer(Color.LightSlateGray);
            Core.scene = SCENE;

            // Create the world
            CreateBaseTiles();
        }

        public static void Update()
        {
            foreach (Tile t in tiles)
            {
                if (t != null) t.Update();
            }
        }

        private static void CreateBaseTiles()
        {
            float[,] noiseMap = Utils.Noise.Calc2D(WORLD_WIDTH, WORLD_HEIGHT, 0.10f);
            tiles = new Tile[WORLD_WIDTH, WORLD_HEIGHT];

            for (int x = 0; x < tiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y < tiles.GetUpperBound(1); y++)
                {
                    // Create the sprite
                    Sprite sprite;
                    if (noiseMap[x, y] < 30f) sprite = new Sprite(Utils.GlobalContent.Earth.Dirt);
                    else if (noiseMap[x, y] > 220f) sprite = new Sprite(Utils.GlobalContent.Earth.Sand);
                    else sprite = new Sprite(Utils.GlobalContent.Earth.Grass);
                    sprite.renderLayer = 20;

                    // Create the entity
                    Entity entity = SCENE.createEntity("Tile at x: " + x + " y: " + y);
                    entity.position = new Vector2(-50 + (x + 1) * 100, -50 + (y + 1) * 100);

                    // Create the tile
                    Tile tile = new Tile(entity, sprite, entity.position, new Vector2(x, y));
                    tiles[x, y] = tile;

                    // Add components
                    entity.addComponent<Sprite>(sprite);
                    entity.addComponent<Tile>(tile);
                }
            }
        }

        public static Tile GetMouseOverTile()
        {
            Tile t = GetTileAt(InputManager.GetScreenWorldPoint());
            return t != null ? t : null;
        }

        public static Tile GetTileAt(Vector2 coord)
        {
            int x = Mathf.floorToInt(coord.X) / 100;
            int y = Mathf.floorToInt(coord.Y) / 100;
            if (x >= WORLD_WIDTH || x < 0 || y >= WORLD_HEIGHT || y < 0)
            {
                return null;
            }
            return tiles[x, y];
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
