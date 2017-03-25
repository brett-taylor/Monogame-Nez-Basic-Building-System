using System.Collections.Generic;

/*
 * Brett Taylor
 * Holds all the tasks related to a type.
 * E.g. WorkmanTaskQueue will hold all tasks that workmen should complete.
 */

namespace HospitalCeo.Tasks
{
    public class TaskQueue<T> : Queue<T>
    {
    }
}
