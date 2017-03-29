using System.Collections.Generic;
using Nez;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
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
        private Queue<Tile> path;
        private Dictionary<Tile, PathfindingNode<Tile>> nodes;

        public PathfindingAStar(Tile tileStart, Tile tileEnd)
        {
            // If we dont have a tile graph for that type of entity create it.
            CreateNewGraph();

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
            
            Priority_Queue.SimplePriorityQueue<PathfindingNode<Tile>> openSet = new Priority_Queue.SimplePriorityQueue<PathfindingNode<Tile>>();
            openSet.Enqueue(nodes[tileStart], 0);
            List<PathfindingNode<Tile>> closedSet = new List<PathfindingNode<Tile>>();
            Dictionary<PathfindingNode<Tile>, PathfindingNode<Tile>> cameFrom = new Dictionary<PathfindingNode<Tile>, PathfindingNode<Tile>>();

            Dictionary<PathfindingNode<Tile>, float> gScore = new Dictionary<PathfindingNode<Tile>, float>();
            foreach (PathfindingNode<Tile> n in nodes.Values)
            {
                gScore[n] = int.MaxValue;
            }
            gScore[nodes[tileStart]] = 0;

            Dictionary<PathfindingNode<Tile>, float> fScore = new Dictionary<PathfindingNode<Tile>, float>();
            foreach (PathfindingNode<Tile> n in nodes.Values)
            {
                fScore[n] = int.MaxValue;
            }
            fScore[nodes[tileStart]] = EstimateCost(nodes[tileStart], nodes[tileEnd]);

            while (openSet.Count > 0)
            {
                PathfindingNode<Tile> current = openSet.Dequeue();
                if (current == nodes[tileEnd])
                {
                    ReconstructPath(cameFrom, current);
                    return;
                }

                closedSet.Add(current);
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
            }

            // Didnt find a route
        }

        private float EstimateCost(PathfindingNode<Tile> a, PathfindingNode<Tile> b)
        {
            return Mathf.sqrt(
                Mathf.pow(a.NodeData.GetTileNumber().X - b.NodeData.GetTileNumber().X, 2) +
                Mathf.pow(a.NodeData.GetTileNumber().X - b.NodeData.GetTileNumber().Y, 2)
            );
        }

        private void ReconstructPath(Dictionary<PathfindingNode<Tile>, PathfindingNode<Tile>> cameFrom, PathfindingNode<Tile> current)
        {
            Queue<Tile> finishedPath = new Queue<Tile>();
            finishedPath.Enqueue(current.NodeData);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                finishedPath.Enqueue(current.NodeData);
            }

            path = new Queue<Tile>(finishedPath.Reverse());
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

        private void CreateNewGraph()
        {
            if (WorldController.PATHFINDING_HUMAN_GRID == null)
                WorldController.PATHFINDING_HUMAN_GRID = new PathfindingHuman();

            nodes = WorldController.PATHFINDING_HUMAN_GRID.Nodes;
        }

        public int Length()
        {
            return path == null ? 0 : path.Count;
        }

        public Tile Dequeue()
        {
            if (path == null) return null;
            if (path.Count == 0) return null;
            return path.Dequeue();
        }
    }
}
