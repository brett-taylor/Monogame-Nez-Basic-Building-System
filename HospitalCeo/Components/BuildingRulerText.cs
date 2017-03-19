using Nez;
using Microsoft.Xna.Framework;

/*
 * Brett Taylor
 * Building ruler gets given a position and size and if size is greater than 2 writes the size
 */

namespace HospitalCeo.Components
{
    public class BuildingRulerText : Component
    {
        private Entity xAxis, yAxis;
        private Text xAxisText, yAxisText;
        private Vector2 position, size;

        public BuildingRulerText() : base()
        {
            xAxis = World.WorldController.SCENE.createEntity("Building ruler text x axis");
            xAxis.setEnabled(false);
            xAxisText = new Text(Graphics.instance.bitmapFont, "", new Vector2(0, 0), Color.White);
            xAxis.scale = new Vector2(3f, 3f);
            xAxis.addComponent<Text>(xAxisText);

            yAxis = World.WorldController.SCENE.createEntity("Building ruler text y axis");
            yAxis.setEnabled(false);
            yAxisText = new Text(Graphics.instance.bitmapFont, "", new Vector2(0, 0), Color.White);
            yAxis.scale = new Vector2(3f, 3f);
            yAxis.addComponent<Text>(yAxisText);
        }

        public void update(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;

            // Horizontal Ruler
            if (size.X >= 3)
            {
                xAxis.setEnabled(true);
                xAxisText.setText(size.X + "m");
                xAxisText.transform.position = new Vector2(position.X - 20, position.Y + ((size.Y * 100) / 2) + 50);
            }
            else xAxis.setEnabled(false);

            // Vertical Ruler
            if (size.Y >= 3)
            {
                yAxis.setEnabled(true);
                yAxisText.setText(size.Y + "m");
                yAxisText.transform.position = new Vector2(position.X - ((size.X * 100) / 2) - 100, position.Y - 20);
            }
            else yAxis.setEnabled(false);
        }

        public override void onEnabled()
        {
            xAxis.setEnabled(true);
            yAxis.setEnabled(true);
        }

        public override void onDisabled()
        {
            xAxis.setEnabled(false);
            yAxis.setEnabled(false);
        }

        public override void onRemovedFromEntity()
        {
            xAxis.destroy();
            yAxis.destroy();
        }
    }
}
