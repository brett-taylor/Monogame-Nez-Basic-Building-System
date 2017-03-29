using System.Collections.Generic;
using HospitalCeo.World;
using Nez;

/*
 * Brett Taylor
 * Instruction
 * This will be claimed by the AI
 * E.G A worker will claim this Instruction - it is up to that single worker to do all of these processes
 */

namespace HospitalCeo.Tasks
{
    public class Instruction
    {
        private Queue<Process> processes;
        protected Entity entity;

        public Instruction()
        {
            processes = new Queue<Process>();
        }

        public void AddProcess(Process process)
        {
            processes.Enqueue(process);
            process.SetInstruction(this);
        }

        public bool IsFinished()
        {
            return processes.Count == 0 ? true : false;
        }

        public Process GetNextProcess(Entity entity)
        {
            if (IsFinished()) return null;
            this.entity = entity;

            return processes.Dequeue();
        }

        public Entity GetEntity()
        {
            return entity;
        }
    }
}
