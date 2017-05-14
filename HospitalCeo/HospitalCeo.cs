using Nez;

/*
 * Brett Taylor
 * Main Game Class
 */

namespace HospitalCeo
{
    public class HospitalCeo : Core, IUpdatableManager
    {
        public HospitalCeo() : base(width: 1000, height: 600, windowTitle: "Some Building Game")
        {
        }

        public void update()
        {
            GameStateManager.Update();
        }

        protected override void Initialize()
        {
            base.Initialize();
            GameStateManager.Initialise(this);
        }
    }
}
