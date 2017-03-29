using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Brett Taylor
 * Acts as a node in the pathfinding algorithm.
 */

namespace HospitalCeo.Pathfinding
{
    public class PathfindingNode<T>
    {
        public T NodeData;
        public List<PathfindingEdge<T>> Edges;

        public PathfindingNode()
        {
            Edges = new List<PathfindingEdge<T>>();
        }
    }
}
