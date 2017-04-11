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
        private Instruction currentInstructions;
        private Process currentProcess;
        private bool isDoingTask;

        public Workman() : base()
        {
            SetMovementSpeed(Nez.Random.range(0.2f, 1.8f));
        }

        private void ReachedJobTile()
        {
            isDoingTask = true;
        }

        void IUpdatable.update()
        {
            if (currentInstructions == null && Task.AnyTasks())
                currentInstructions = Task.GetNextInstruction();

            if (currentInstructions != null)
            {
                if (currentInstructions.IsFinished())
                    currentInstructions = null;

                if (currentProcess == null & currentInstructions != null)
                {
                    currentProcess = currentInstructions.GetNextProcess(this.entity);
                    pathfinder.SetDestination(this, currentProcess.GetWorkTile().GetPosition());
                    pathfinder.RegisterOnReachHandle(ReachedJobTile);
                }
            }

            if (isDoingTask && currentProcess != null)
            {
                currentProcess.DoProcess(Time.deltaTime);
                if (currentProcess.GetRemainingTime() < 0)
                {
                    currentProcess = null;
                    isDoingTask = false;
                }
            }
        }
    }
}
