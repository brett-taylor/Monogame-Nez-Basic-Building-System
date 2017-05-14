using System.Collections.Generic;

/*
 * Brett Taylor
 * Acts as a node in the pathfinding algorithm.
 */

namespace HospitalCeo.Pathfinding
{
    public class PathfindingNode<T> : Priority_Queue.FastPriorityQueueNode
    {
        public T NodeData;
        public List<PathfindingEdge<T>> Edges;

        public PathfindingNode()
        {
            Edges = new List<PathfindingEdge<T>>();
        }
    }
}
