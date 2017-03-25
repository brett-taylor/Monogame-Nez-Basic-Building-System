using System.Collections.Generic;

/*
 * Brett Taylor
 * Manager class for task related stuff
 */
namespace HospitalCeo.Tasks
{
    public static class TaskManager
    {
        public static TaskQueue<Task> WORKMAN_TASK_QUEUE;
        public static Task currentTask;

        public static void Initialise()
        {
            WORKMAN_TASK_QUEUE = new TaskQueue<Task>();
        }

        public static void Update()
        {
        }
    }
}
