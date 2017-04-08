using System;
using System.Collections.Generic;
using Nez;
using HospitalCeo.World;
using HospitalCeo.AI;

/*
 * Brett Taylor
 * PathfindManager handles entities requests for a pathfind route
 * Limits the amount so they occur over multiple frames and not all on one frame.
 */

namespace HospitalCeo.Pathfinding
{
    public static class PathfindManager
    {
        private static Priority_Queue.SimplePriorityQueue<PathfindTask> pathfindQueue;
        private static PathfindTask currentPathfind;
        private static float timeLeft = 0f;

        public static void Initialise()
        {
            pathfindQueue = new Priority_Queue.SimplePriorityQueue<PathfindTask>();
        }

        public static void Update()
        {
            // Handles getting the current pathfindTask
            if (currentPathfind == null && pathfindQueue.Count > 0)
            {
                currentPathfind = pathfindQueue.Dequeue();
                return;
            }

            if (currentPathfind != null)
            {
                if (currentPathfind.GetStatus() == PathfindTaskStatus.DONE)
                {
                    currentPathfind.GetPathfindComponent().AllowedToPathfind(currentPathfind.GetPathfind().GetFinishedPath());
                    currentPathfind = null;
                    return;
                }

                currentPathfind.Update();
                return;
            }
        }

        public static void RequestPathfind(PathfindPriority priority, PathfindComponent pathfinder, Tile startTile, Tile endTile)
        {
            pathfindQueue.Enqueue(new PathfindTask(pathfinder, startTile, endTile), (int) priority);
        }
    }
}
