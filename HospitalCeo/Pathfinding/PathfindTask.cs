using System;
using Nez;
using HospitalCeo.World;
using HospitalCeo.AI;

namespace HospitalCeo.Pathfinding
{
    public class PathfindTask
    {
        private PathfindingAStar pathfind;
        private PathfindComponent pathfindComponent;
        private Tile startTile;
        private Tile endTile;
        private PathfindTaskStatus status = PathfindTaskStatus.CREATE_VARIABLES;

        public PathfindTask(PathfindComponent pathfindComponent, Tile startTile, Tile endTile)
        {
            this.pathfindComponent = pathfindComponent;
            this.startTile = startTile;
            this.endTile = endTile;
        }

        public PathfindTaskStatus GetStatus()
        {
            return status;
        }

        public PathfindingAStar GetPathfind()
        {
            return pathfind;
        }

        public PathfindComponent GetPathfindComponent()
        {
            return pathfindComponent;
        }

        public void Update()
        {
            if (pathfind == null)
            {
                pathfind = new PathfindingAStar(startTile, endTile);
                return;
            }

            if (status == PathfindTaskStatus.CREATE_VARIABLES)
            {
                pathfind.CreateVariables();
                status = PathfindTaskStatus.CALCULATE_G_SCORES;
                return;
            }

            if (status == PathfindTaskStatus.CALCULATE_G_SCORES)
            {
                pathfind.CalculateGScores();
                status = PathfindTaskStatus.CALCULATE_F_SCORES;
                return;
            }

            if (status == PathfindTaskStatus.CALCULATE_F_SCORES)
            {
                pathfind.CalculateFScores();
                status = PathfindTaskStatus.CALCULATE_PATH;
                return;
            }

            if (status == PathfindTaskStatus.CALCULATE_PATH)
            {
                if (pathfind.PathFound())
                {
                    status = PathfindTaskStatus.DONE;
                    return;
                }
                else
                {
                    pathfind.CalculatePath(70);
                    return;
                }
            }
        }
    }
}
