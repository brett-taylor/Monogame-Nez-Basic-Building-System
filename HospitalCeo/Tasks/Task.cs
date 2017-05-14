using System.Collections.Generic;

/*
 * Brett Taylor
 * Task
 */

namespace HospitalCeo.Tasks
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class Task
    {
        private Queue<Subtask> subtasks;

        public Task()
        {
            // Create the subtasks list
            subtasks = new Queue<Subtask>();

            // If the tasks list doesnt exist create it and add itself
            if (tasks == null) tasks = new Queue<Task>();
            tasks.Enqueue(this);
        }

        public void AddSubtask(Subtask subtask)
        {
            subtasks.Enqueue(subtask);
        }

        public bool IsFinished()
        {
            return subtasks.Count == 0 ? true : false;
        }

        public Subtask GetNextSubtask()
        {
            if (IsFinished()) return null;

            return subtasks.Dequeue();
        }

        /*
         * Start of the static Task methods
         */
        private static Queue<Task> tasks;
        private static Task currentTask;
        private static Subtask currentSubtask;

        public static bool AnyTasks()
        {
            // Check if we have a sub task currently, then check if that sub task is finished or not
            if (currentSubtask != null)
                if (!currentSubtask.IsFinished())
                    return true;

            // Check if we have a task currently, then check if it has any sub tasks lefts
            if (currentTask != null)
                if (!currentTask.IsFinished())
                    return true;

            // If tasks is greater than 0
            if (tasks != null && tasks.Count > 0)
                return true;

            return false;
        }

        public static Task GetCurrentTask()
        {
            return currentTask;
        }

        public static Subtask GetCurrentSubtask()
        {
            return currentSubtask;
        }

        public static Instruction GetNextInstruction()
        {
            if (currentSubtask != null)
                if (!currentSubtask.IsFinished())
                    return currentSubtask.GetNextInstruction();

            if (currentTask != null)
                if (!currentTask.IsFinished())
                {
                    currentSubtask = currentTask.GetNextSubtask();
                    return GetNextInstruction();
                }

            if (tasks.Count > 0)
            {
                currentTask = tasks.Dequeue();
                return GetNextInstruction();
            }

            return null;
        }

        public static void Clear()
        {
            tasks.Clear();
            currentTask = null;
            currentSubtask = null;
        }
    }
}
