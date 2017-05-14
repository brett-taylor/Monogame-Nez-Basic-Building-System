using HospitalCeo.World;
using Nez;
using System.Collections.Generic;
using System.Linq;

/*
 * Brett Taylor
 * Pathfinding.
 * https://en.wikipedia.org/wiki/A*_search_algorithm
 */

namespace HospitalCeo.Pathfinding
{
    public class PathfindingAStar
    {
        /*
         * To DO
         * Turn openSet into a FastPriorityQueue
         */

        private Tile tileStart, tileEnd;
        private List<Tile> path;
        private Dictionary<Tile, PathfindingNode<Tile>> nodes;

        private Priority_Queue.FastPriorityQueue<PathfindingNode<Tile>> openSet;
        private Priority_Queue.FastPriorityQueue<PathfindingNode<Tile>> closedSet;

        private Dictionary<PathfindingNode<Tile>, PathfindingNode<Tile>> cameFrom;
        private Dictionary<PathfindingNode<Tile>, float> gScore;
        private Dictionary<PathfindingNode<Tile>, float> fScore;
        private bool isFound = false;

        public PathfindingAStar(Tile tileStart, Tile tileEnd)
        {
            nodes = WorldController.PATHFINDING_HUMAN_GRID.Nodes;

            if (tileStart == null || tileEnd == null)
            {
                Nez.Console.DebugConsole.instance.log("Pathfinding: Starting tile or ending tile is null. Start tile: " + (tileStart == null ? "Is null" : tileStart.ToString()) + ", end tile: " + (tileEnd == null? "Is null" : tileEnd.ToString()));
                Nez.Console.DebugConsole.instance.Open();
                return;
            }

            if (nodes.ContainsKey(tileStart) == false || nodes.ContainsKey(tileEnd) == false) // Check we have both the start and end tile in the list
            {
                Nez.Console.DebugConsole.instance.log("Pathfinding: Start/Ending tile isnt in list. Start Node: " + tileStart + " End Tile: " + tileEnd);
                Nez.Console.DebugConsole.instance.Open();
                return;
            }

            this.tileStart = tileStart;
            this.tileEnd = tileEnd;
        }

        public void CreateVariables()
        {
            openSet = new Priority_Queue.FastPriorityQueue<PathfindingNode<Tile>>(10000);
            openSet.Enqueue(nodes[tileStart], 0);
            closedSet = new Priority_Queue.FastPriorityQueue<PathfindingNode<Tile>>(10000);
            cameFrom = new Dictionary<PathfindingNode<Tile>, PathfindingNode<Tile>>();
            gScore = new Dictionary<PathfindingNode<Tile>, float>();
            fScore = new Dictionary<PathfindingNode<Tile>, float>();
        }

        public void CalculateGScores()
        {
            foreach (PathfindingNode<Tile> n in nodes.Values)
                gScore[n] = int.MaxValue;

            gScore[nodes[tileStart]] = 0;
        }

        public void CalculateFScores()
        {
            foreach (PathfindingNode<Tile> n in nodes.Values)
                fScore[n] = int.MaxValue;

            fScore[nodes[tileStart]] = EstimateCost(nodes[tileStart], nodes[tileEnd]);
        }

        public void CalculatePath(int amountOfLoops)
        {
            int loops = 0;
            int maximumLoops = amountOfLoops;
            while (openSet.Count > 0 & loops < maximumLoops)
            {
                PathfindingNode<Tile> current = openSet.Dequeue();
                if (current == nodes[tileEnd])
                {
                    ReconstructPath(cameFrom, current);
                    return;
                }

                closedSet.Enqueue(current, 0);
                foreach (PathfindingEdge<Tile> edgeNeighbour in current.Edges)
                {
                    if (edgeNeighbour == null) continue;

                    PathfindingNode<Tile> neighbour = edgeNeighbour.PathNode;
                    if (closedSet.Contains(neighbour))
                        continue;

                    float movementCost = neighbour.NodeData.GetMovementCost() * DistanceBetween(current, neighbour);
                    float tentativeGScore = gScore[current] + movementCost;

                    if (openSet.Contains(neighbour) && tentativeGScore >= gScore[neighbour])
                        continue;

                    cameFrom[neighbour] = current;
                    gScore[neighbour] = tentativeGScore;
                    fScore[neighbour] = gScore[neighbour] + EstimateCost(neighbour, nodes[tileEnd]);

                    if (openSet.Contains(neighbour) == false)
                        openSet.Enqueue(neighbour, fScore[neighbour]);
                }
                loops++;
            }
        }

        public float EstimateCost(PathfindingNode<Tile> a, PathfindingNode<Tile> b)
        {
            return Mathf.sqrt(
                Mathf.pow(a.NodeData.GetTileNumber().X - b.NodeData.GetTileNumber().X, 2) +
                Mathf.pow(a.NodeData.GetTileNumber().X - b.NodeData.GetTileNumber().Y, 2)
            );
        }

        private void ReconstructPath(Dictionary<PathfindingNode<Tile>, PathfindingNode<Tile>> cameFrom, PathfindingNode<Tile> current)
        {
            isFound = true;
            List<Tile> finishedPath = new List<Tile>();
            finishedPath.Add(current.NodeData);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                finishedPath.Add(current.NodeData);
            }

            finishedPath.Reverse();
            path = finishedPath;
        }

        private float DistanceBetween(PathfindingNode<Tile> a, PathfindingNode<Tile> b)
        {
            // Up, down, left, right
            if (System.Math.Abs(a.NodeData.GetTileNumber().X - b.NodeData.GetTileNumber().X) + System.Math.Abs(a.NodeData.GetTileNumber().Y - b.NodeData.GetPosition().Y) == 1)
                return 1f;

            // Diag
            if (System.Math.Abs(a.NodeData.GetTileNumber().X - b.NodeData.GetTileNumber().X) == 1 && System.Math.Abs(a.NodeData.GetTileNumber().Y - b.NodeData.GetPosition().Y) == 1)
                return 1.41421356237f;

            return Mathf.sqrt(
                Mathf.pow(a.NodeData.GetPosition().X - b.NodeData.GetPosition().X, 2) +
                Mathf.pow(a.NodeData.GetPosition().Y - b.NodeData.GetPosition().Y, 2)
            );
        }

        public int Length()
        {
            return path == null ? 0 : path.Count;
        }

        public bool PathFound()
        {
            return isFound;
        }

        public Tile[] GetFinishedPath()
        {
            return path.ToArray<Tile>();
        }
    }
}
