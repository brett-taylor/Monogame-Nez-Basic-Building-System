using System;
using System.Collections.Generic;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace HospitalCeo.Building
{
    public class BuildingSprite : Sprite
    {
        private Vector2 position;
        public int percentageBuilt = 0;

        public BuildingSprite(Subtexture texture, Vector2 position) : base(texture)
        {
            this.position = position;
        }

        public override void render(Graphics graphics, Camera camera)
        {
            if (percentageBuilt < 80)
            {
                graphics.batcher.drawHollowRect(new Rectangle((int) position.X + 10, (int) position.Y + 10, 80, 80), Color.White, thickness: 5);
                graphics.batcher.drawRect(new Rectangle((int) position.X + 10, (int) position.Y + 10 + (80 - percentageBuilt), 80, percentageBuilt), new Color(Color.Green, 0.3f));
            }
            else
            {
                base.render(graphics, camera);
            }
        }

        public override void debugRender(Graphics graphics)
        {
        }

        public void SetPercentageBuilt(int percentage)
        {
            percentageBuilt = percentage;
        }
    }
}
