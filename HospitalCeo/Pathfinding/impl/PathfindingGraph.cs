using System;
using System.Collections.Generic;
using HospitalCeo.World;
using Nez;
using Microsoft.Xna.Framework;

/*
 * Brett Taylor
 * All pathfinding node graphs inherit from this.
 */

namespace HospitalCeo.Pathfinding
{
    public class PathfindingGraph
    {
        public Dictionary<Tile, PathfindingNode<Tile>> Nodes;

        // Create the node then create the edges
        protected void AddNode(Tile t, bool shouldCreateEdges)
        {
            PathfindingNode<Tile> n = new PathfindingNode<Tile>();
            n.NodeData = t;
            Nodes.Add(t, n);
            t.AddToPathfind(n);
            if (shouldCreateEdges) RebuildEdges(t);
        }

        // Remove all the edges involving the tile, then remove the node
        protected void RemoveNode(Tile t)
        {
            if (Nodes.ContainsKey(t))
            {
                Tile north = t.GetNeighbour(Compass.N);
                if (north != null) RemoveEdgeBetween(north, t);

                Tile east = t.GetNeighbour(Compass.E);
                if (east != null) RemoveEdgeBetween(east, t);

                Tile south = t.GetNeighbour(Compass.S);
                if (south != null) RemoveEdgeBetween(south, t);

                Tile west = t.GetNeighbour(Compass.W);
                if (west != null) RemoveEdgeBetween(west, t);

                Nodes.Remove(t);
                t.RemoveFromPathfind();
            }
        }

        public void RebuildAllEdges()
        {
            foreach (Tile t in Nodes.Keys)
                RebuildEdges(t);
        }

        protected void RebuildEdges(Tile t)
        {
            // Firstly create the edges from the tile to neighbouring tiles
            PathfindingNode<Tile> n = Nodes[t];
            List<PathfindingEdge<Tile>> edges = new List<PathfindingEdge<Tile>>();
            Tile[] neighbours = t.GetNeighbours(false);

            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i] != null && neighbours[i].GetMovementCost() > 0 && IsClippingCorner(t, neighbours[i]) == false)
                {
                    PathfindingEdge<Tile> e = new PathfindingEdge<Tile>();
                    e.Cost = neighbours[i].GetMovementCost();
                    e.PathNode = Nodes[neighbours[i]];
                    edges.Add(e);
                }
            }

            n.Edges = edges;

            Tile north = t.GetNeighbour(Compass.N);
            if (north != null)
                AddEdgeBetween(north, t);

            Tile east = t.GetNeighbour(Compass.E);
            if (east != null)
                AddEdgeBetween(east, t);

            Tile south = t.GetNeighbour(Compass.S);
            if (south != null)
                AddEdgeBetween(south, t);

            Tile west = t.GetNeighbour(Compass.W);
            if (west != null)
                AddEdgeBetween(west, t);
        }

        private void AddEdgeBetween(Tile tileFrom, Tile tileTo)
        {
            PathfindingNode<Tile> tileFromNode;
            Nodes.TryGetValue(tileFrom, out tileFromNode);
            PathfindingNode<Tile> tileToNode;
            Nodes.TryGetValue(tileTo, out tileToNode);

            if (tileFromNode == null || tileToNode == null)
                return;

            PathfindingEdge<Tile> e = new PathfindingEdge<Tile>();
            e.Cost = tileTo.GetMovementCost();
            e.PathNode = tileToNode;
            tileFromNode.Edges.addIfNotPresent(e);
        }

        private void RemoveEdgeBetween(Tile tileFrom, Tile tileTo)
        {
            PathfindingNode<Tile> tileFromNode;
            Nodes.TryGetValue(tileFrom, out tileFromNode);
            PathfindingNode<Tile> tileToNode = Nodes[tileTo];
            Nodes.TryGetValue(tileTo, out tileToNode);

            if (tileFromNode == null || tileToNode == null)
                return;

            if (tileFromNode != null)
            {
                for (int i = 0; i < tileFromNode.Edges.Count; i++)
                {
                    if (tileFromNode.Edges[i] != null)
                        if (tileFromNode.Edges[i].PathNode.NodeData == tileTo)
                            tileFromNode.Edges[i] = null;
                }
            }
        }

        public void RebuildTile(Tile t)
        {
            RemoveNode(t);
            if (t.GetMovementCost() != 99)
               AddNode(t, true);
        }

        protected bool IsClippingCorner(Tile current, Tile neighbour)
        {
            int dX = (int) (current.GetTileNumber().X - neighbour.GetTileNumber().X);
            int dY = (int) (current.GetTileNumber().Y - neighbour.GetTileNumber().Y);

            if (Math.Abs(dX) + Math.Abs(dY) == 2)
            {
                if (WorldController.GetTileAt((int) current.GetTileNumber().X - dX, (int) current.GetTileNumber().Y).GetMovementCost() == 0)
                    return true;

                if (WorldController.GetTileAt((int) current.GetTileNumber().X, (int) current.GetTileNumber().Y - dY).GetMovementCost() == 0)
                    return true;
            }

            return false;
        }
    }
}
