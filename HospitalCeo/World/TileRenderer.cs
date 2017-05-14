using Nez;

namespace HospitalCeo.World
{
    public class TileRenderer : Component
    {
        public static bool DRAW_TILE_HOVER_INFORMATION = false;
        //private TileSprite[,] sprites;

        public override void onAddedToEntity()
        {
            /*sprites = new TileSprite[WorldController.WORLD_WIDTH, WorldController.WORLD_HEIGHT];

            for (int x = 0; x < WorldController.tiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y < WorldController.tiles.GetUpperBound(1); y++)
                {
                    Tile t = WorldController.GetTileAt(x, y);
                    TileSprite newSprite = new TileSprite(t, t.GetTexture());
                    newSprite.localOffset = new Vector2(t.GetPosition().X, t.GetPosition().Y);
                    newSprite.renderLayer = 20;
                    entity.addComponent<Sprite>(newSprite);

                    sprites[x, y] = newSprite;
                }
            }*/
        }

        public override void debugRender(Graphics graphics)
        {
            if (!DRAW_TILE_HOVER_INFORMATION) return;
            Tile t = WorldController.GetMouseOverTile();
            if (t != null)
            {
                Debug.drawText(" [Tile] Tile Infrastructure: " + t.GetInfrastructureItem(), duration: 0);
                Debug.drawText(" [Tile] Tile Position: " + t.GetTileNumber() + " : " + t.GetPosition(), duration: 0);
            }
        }
    }
}
