using System;
using HospitalCeo.World;

/*
 * Brett Taylor
 * Action
 * This is the actual action that the task wants performed
 * E.G. Remove tree, Construct floor
 */

namespace HospitalCeo.Tasks
{
    public class Process
    {
        private Tile tile;
        private float actionTime;
        private float remainingTime;
        private Instruction instruction;

        private Action<Process> onActionCompleted;
        private Action<Process> onActionTicked;

        public Process(Tile tile, float actionTime, Action<Process> onActionCompleted)
        {
            this.tile = tile;
            this.actionTime = actionTime;
            this.remainingTime = actionTime;
            this.onActionCompleted += onActionCompleted;
        }

        public void RegisterOnCompleteHandle(Action<Process> callback)
        {
            onActionCompleted += callback;
        }

        public void RegisterOnTickHandle(Action<Process> callback)
        {
            onActionTicked += callback;
        }

        public void DoProcess(float workAmount)
        {
            remainingTime -= workAmount;
            onActionTicked?.Invoke(this);

            if (remainingTime <= 0)
            {
                onActionCompleted?.Invoke(this);
            }
        }

        public Tile GetWorkTile()
        {
            return tile;
        }

        public float GetProcessTime()
        {
            return actionTime;
        }

        public float GetRemainingTime()
        {
            return remainingTime;
        }

        public void SetInstruction(Instruction instruction)
        {
            this.instruction = instruction;
        }

        public Instruction GetInstruction()
        {
            return instruction;
        }
    }
}
