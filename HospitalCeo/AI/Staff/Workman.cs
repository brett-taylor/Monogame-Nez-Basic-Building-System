using System;
using Nez;
using Microsoft.Xna.Framework;
using Nez.Textures;
using HospitalCeo.World;
using HospitalCeo.Tasks;

/*
 * Workman Class
 */

namespace HospitalCeo.AI.Staff
{
    public class Workman : Staff, IUpdatable
    {
        private Task buildTask;
        private bool isDoingTask;

        public Workman(Vector2 position) : base(position)
        {
        }

        public override string GetName()
        {
            return "Worker";
        }

        public override Subtexture GetNorthFacingSprite()
        {
            return Utils.GlobalContent.Worker.Worker_North;
        }

        public override Subtexture GetEastFacingSprite()
        {
            return Utils.GlobalContent.Worker.Worker_West;
        }

        public override Subtexture GetSouthFacingSprite()
        {
            return Utils.GlobalContent.Worker.Worker_South;
        }

        public override Subtexture GetWestFacingSprite()
        {
            return Utils.GlobalContent.Worker.Worker_West;
        }

        public override bool UsesEastSprite()
        {
            return false;
        }

        public void update()
        {
            if (buildTask == null)
            {
                if (TaskManager.WORKMAN_TASK_QUEUE.Count >= 1)
                {
                    buildTask = TaskManager.WORKMAN_TASK_QUEUE.Dequeue();
                    pathfinder.SetDestinationTile(buildTask.GetTile());
                    pathfinder.RegisterOnReachHandle(ReachedJobTile);
                }
            }

            if (isDoingTask == true)
            {
                buildTask.StartTask(Time.deltaTime);
                if (buildTask.GetTimeLeft() < 0)
                {
                    buildTask = null;
                    isDoingTask = false;
                }
            }
        }

        private void ReachedJobTile()
        {
            isDoingTask = true;
        }
    }
}
