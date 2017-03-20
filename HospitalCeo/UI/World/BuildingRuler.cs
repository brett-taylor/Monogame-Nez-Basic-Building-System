using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HospitalCeo.World;
using System;

/*
 * Brett Taylor
 * Building ruler, used when building shows user the size of their flexible sized buildings component
 */

namespace HospitalCeo.UI.World
{
    public class BuildingRuler : RenderableComponent
    {
        private bool isYAxis;
        private LineRenderer line;
        private Vector2 position;

        public BuildingRuler(bool isYAxis)
        {
        }

        public override void render(Graphics graphics, Camera camera)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int y = 0; y < 100; y++)
                {
                    graphics.batcher.drawPixel(new Vector2(i, y), Color.Red);
                }
            }
        }

        public void Update(Vector2 position, Vector2 size)
        {
            this.position = position;
        }
    }
}
