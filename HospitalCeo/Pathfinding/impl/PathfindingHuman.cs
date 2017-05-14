using HospitalCeo.World;
using System.Collections.Generic;

/*
 * Brett Taylor
 * Creates the graph that most entities will use.
 */

namespace HospitalCeo.Pathfinding
{
    public class PathfindingHuman : PathfindingGraph
    {
        public PathfindingHuman()
        {
            Nodes = new Dictionary<Tile, PathfindingNode<Tile>>();

            for (int x = 0; x < WorldController.WORLD_WIDTH; x++)
            {
                for (int y = 0; y < WorldController.WORLD_HEIGHT; y++)
                {
                    Tile t = WorldController.GetTileAt(x, y);
                    if (t == null) continue;
                    AddNode(t, false);
                }
            }

            RebuildAllEdges();
        }
    }
}
