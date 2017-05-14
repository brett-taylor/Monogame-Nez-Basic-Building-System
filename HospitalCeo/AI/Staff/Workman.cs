using HospitalCeo.Tasks;
using Nez;

/*
 * Workman Class
 */

namespace HospitalCeo.AI.Staff
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class Workman : Staff, IUpdatable
    {
        private Instruction currentInstructions;
        private Process currentProcess;
        private bool isDoingTask;
        private bool canTakeJobs = true;

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
            if (currentInstructions == null && Task.AnyTasks() && canTakeJobs)
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

        public void SetCurrentInstruction(Instruction instruction)
        {
            currentInstructions = instruction;
        }

        public void SetCanTakeJobs(bool canTakeJobs)
        {
            this.canTakeJobs = canTakeJobs;
        }

        public bool CanTakeJobs()
        {
            return this.canTakeJobs;
        }
    }
}
