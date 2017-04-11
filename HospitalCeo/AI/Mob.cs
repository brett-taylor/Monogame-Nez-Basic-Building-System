using System;
using Nez;
using Microsoft.Xna.Framework;
using HospitalCeo.World;

/*
 * Default mob class
 */

namespace HospitalCeo.AI
{
    public class Mob : Component
    {
        protected PathfindComponent pathfinder;
        protected float movementSpeed = 1f;

        public Mob() : base()
        {
        }

        public override void onAddedToEntity()
        {
            pathfinder = entity.addComponent<PathfindComponent>(new PathfindComponent());
        }

        public virtual string GetName()
        {
            return "No-one";
        }

        public void SetMovementSpeed(float newSpeed)
        {
            this.movementSpeed = newSpeed;
        }

        public float GetMovementSpeed()
        {
            return movementSpeed;
        }
    }
}
