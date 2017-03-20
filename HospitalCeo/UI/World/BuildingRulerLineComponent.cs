using Nez;
using Microsoft.Xna.Framework;

/*
 * Brett Taylor
 * Building ruler gets given a position and size and if size is greater than 2 on that axis draws it.
 */

namespace HospitalCeo.UI.World
{
    public class BuildingRulerLineComponent : RenderableComponent
    {
        public override float width { get { return 3000; } } // Have to be called as part of RenderableComponent
        public override float height { get { return 3000; } } // Have to be called as part of RenderableComponent
        private Vector2 position, size;

        public BuildingRulerLineComponent() : base()
        {
        }

        public override void render(Graphics graphics, Camera camera)
        {
            if (size.X >= 2 | size.Y >= 2)
            {
                // Horizontal Ruler
                if (size.X >= 200)
                {
                    Vector2 startPosition = new Vector2(position.X - 60, position.Y + size.Y + 20);
                    Vector2 endPosition = new Vector2(startPosition.X + size.X + 20, startPosition.Y);
                    graphics.batcher.drawLine(startPosition, endPosition, Color.White, 3f); // Middle bit
                    graphics.batcher.drawLine(new Vector2(startPosition.X, startPosition.Y - 10), new Vector2(startPosition.X, startPosition.Y + 10), Color.White, 5f); // Left end
                    graphics.batcher.drawLine(new Vector2(endPosition.X, endPosition.Y - 10), new Vector2(endPosition.X, endPosition.Y + 10), Color.White, 5f); // Right end
                }

                // Vertical Ruler
                if(size.Y >= 200)
                {
                    Vector2 startPosition = new Vector2(position.X - 100, position.Y - 60);
                    Vector2 endPosition = new Vector2(startPosition.X, startPosition.Y + size.Y + 20);
                    graphics.batcher.drawLine(startPosition, endPosition, Color.White, 3f); // Middle bit
                    graphics.batcher.drawLine(new Vector2(startPosition.X - 10, startPosition.Y), new Vector2(startPosition.X + 10, startPosition.Y), Color.White, 5f); // Top end
                    graphics.batcher.drawLine(new Vector2(endPosition.X - 10, endPosition.Y), new Vector2(endPosition.X + 10, endPosition.Y), Color.White, 5f); // Bottom end
                }
            }
        }

        public void update(Vector2 position, Vector2 size)
        {
            System.Diagnostics.Debug.WriteLine(position + "" + size);
            this.position = position;
            this.size = size;
        }
    }
}
