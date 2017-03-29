using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;

/* 
 * Brett Taylor
 * Inherits sprite
 * Disables the normal debug render
 * When there are 10,000+ tiles, debug render will freeze the game
 */

namespace HospitalCeo.World
{
    public class TileSprite : Sprite
    {
        public static bool drawInfrastructureStatus = false;
        public static bool drawPathfindingInformation = false;
        private Tile tile; 

        public TileSprite(Tile tile, Subtexture texture) : base(texture)
        {
            this.tile = tile;
        }

        public override void debugRender(Graphics graphics)
        {
            if (drawInfrastructureStatus)
            {
                Color color;
                if (tile.GetInfrastructureItem() != null) color = new Color(Color.Blue, 0.1f);
                else color = new Color(Color.Red, 0.1f);
                graphics.batcher.drawRect(new Rectangle((int) tile.GetPosition().X - 10, (int) tile.GetPosition().Y - 10, 20, 20), color);
            }

            if (drawPathfindingInformation)
            {
                if (!tile.CanPathfindTo())
                    return;

                Pathfinding.PathfindingNode<Tile> node = tile.GetPathfindNode();
                graphics.batcher.drawRect(new Rectangle((int) tile.GetPosition().X - 10, (int) tile.GetPosition().Y - 10, 20, 20), Color.HotPink);

                foreach (Pathfinding.PathfindingEdge<Tile> edge in node.Edges)
                {
                    if (edge != null)
                        graphics.batcher.drawLine(tile.GetPosition(), edge.PathNode.NodeData.GetPosition(), Color.OrangeRed, thickness: 4);
                }
            }
        }
    }
}
