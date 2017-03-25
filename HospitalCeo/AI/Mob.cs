using System;
using Nez;
using Microsoft.Xna.Framework;

/*
 * Default mob class
 */

namespace HospitalCeo.AI
{
    public class Mob : Component
    {
        protected Vector2 startingPosition;
        protected PathfindingComponent pathfinder;

        public Mob(Vector2 position) : base()
        {
            startingPosition = position;
        }

        public override void onAddedToEntity()
        {
            entity.position = startingPosition;
            pathfinder = new PathfindingComponent();
            entity.addComponent<PathfindingComponent>(pathfinder);
        }

        public virtual string GetName()
        {
            return "No-one";
        }
    }
}
