using HospitalCeo.World;

/*
 * Brett Taylor
 * Used for the FastPriorityQueueNode
 * Used to store PathfindNode<Tile>>
 */

namespace HospitalCeo.Pathfinding
{
    public class FastPriorityQueuePathfindNode : Priority_Queue.FastPriorityQueueNode
    {
        public PathfindingNode<Tile> node;

        public FastPriorityQueuePathfindNode(PathfindingNode<Tile> node)
        {
            this.node = node;
        }
    }
}
