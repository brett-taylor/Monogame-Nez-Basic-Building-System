/*
 * Brett Taylor
 * Acts as a edge in the pathfinding algorithm.
 */

namespace HospitalCeo.Pathfinding
{
    public class PathfindingEdge<T>
    {
        public float Cost;
        public PathfindingNode<T> PathNode;
    }
}
