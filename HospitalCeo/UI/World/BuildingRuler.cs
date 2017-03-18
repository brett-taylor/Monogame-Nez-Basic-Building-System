using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HospitalCeo.World;

/*
 * Brett Taylor
 * Building ruler, used when building shows user the size of their flexible sized buildings
 */

namespace HospitalCeo.UI.World
{
    public class BuildingRuler
    {
        private bool isYAxis;
        private LineRenderer line;

        public BuildingRuler(bool isYAxis)
        {
            this.isYAxis = isYAxis;
            line = new LineRenderer();
            line.setStartEndWidths(100f, 1000f);
            line.setColor(Color.Red);
            line.setEnabled(true);
            line.renderLayer = 5;
        }

        public void Update(Vector2 position, Vector2 size)
        {
        }
    }
}
