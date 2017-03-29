using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
