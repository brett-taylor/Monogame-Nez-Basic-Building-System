using System;
using HospitalCeo.World;

/*
 * Brett Taylor
 * Holds information for a queued up job,
 * E.G Construct a building.
 */

namespace HospitalCeo.Tasks
{
    public class Task
    {
        private Tile tile;
        private float jobTime;
        private float maximumJobTime;

        private Action<Task> OnJobCompleted;
        private Action<Task> onJobCancelled;
        private Action<Task> onJobTicked;

        public Task(Tile tile, float jobTime, Action<Task> OnJobCompleted)
        {
            this.tile = tile;
            this.maximumJobTime = jobTime;
            this.jobTime = this.maximumJobTime;
            this.OnJobCompleted += OnJobCompleted;
        }

        public void RegisterTaskCompleted(Action<Task> callBack)
        {
            OnJobCompleted += callBack;
        }

        public void RegisterTaskUpdate(Action<Task> callBack)
        {
            onJobTicked += callBack;
        }

        public void StartTask(float work)
        {
            jobTime -= work;
            onJobTicked?.Invoke(this);

            if (jobTime <= 0)
            {
                OnJobCompleted?.Invoke(this);
            }
        }

        public void CancelJob()
        {
            onJobCancelled?.Invoke(this);
        }

        public Tile GetTile()
        {
            return tile;
        }

        public float GetTimeLeft()
        {
            return jobTime;
        }

        public float GetTimeMaximum()
        {
            return maximumJobTime;
        }
    }
}
