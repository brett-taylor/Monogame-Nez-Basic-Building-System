/*
 * Staff Class
 */

namespace HospitalCeo.AI.Staff
{
    public abstract class Staff : Mob
    {
        public Staff() : base()
        {
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
        }

        public override string GetName()
        {
            return "Staff No-one";
        }
    }
}
