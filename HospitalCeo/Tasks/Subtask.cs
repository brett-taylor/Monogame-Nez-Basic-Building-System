using System.Collections.Generic;
using HospitalCeo.World;

/*
 * Brett Taylor
 * Subtask
 * This will contain instructions
 * Any worker can claim a instruction
 */

namespace HospitalCeo.Tasks
{
    public class Subtask
    {
        private Queue<Instruction> instructions;

        public Subtask()
        {
            instructions = new Queue<Instruction>();
        }

        public void AddInstruction(Instruction instruction)
        {
            instructions.Enqueue(instruction);
        }

        public bool IsFinished()
        {
            return instructions.Count == 0 ? true : false;
        }

        public Instruction GetNextInstruction()
        {
            if (IsFinished()) return null;

            return instructions.Dequeue();
        }
    }
}
