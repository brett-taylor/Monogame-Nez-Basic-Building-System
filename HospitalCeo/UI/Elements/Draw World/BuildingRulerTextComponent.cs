using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;

/*
 * Brett Taylor
 * Building ruler gets given a position and size and if size is greater than 2 writes the size
 */

namespace HospitalCeo.UI.World
{
    public class BuildingRulerTextComponent : Component
    {
        private Entity xAxis, yAxis;
        private Text xAxisText, yAxisText;
        private Vector2 position, size;

        public BuildingRulerTextComponent() : base()
        {
            xAxis = WorldController.SCENE.createEntity("Building ruler text x axis");
            xAxis.setEnabled(false);
            xAxisText = new Text(Graphics.instance.bitmapFont, "", new Vector2(0, 0), Color.White);
            xAxis.scale = new Vector2(3f, 3f);
            xAxis.addComponent<Text>(xAxisText);

            yAxis = WorldController.SCENE.createEntity("Building ruler text y axis");
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
            if (size.X >= 300)
            {
                xAxis.setEnabled(true);
                xAxisText.setText(size.X / 100 + "m");
                xAxisText.transform.position = new Vector2(position.X + (size.X / 2) - 70, position.Y + size.Y + 50);
            }
            else xAxis.setEnabled(false);

            // Vertical Ruler
            if (size.Y >= 300)
            {
                yAxis.setEnabled(true);
                yAxisText.setText(size.Y /100 + "m");
                yAxisText.transform.position = new Vector2(position.X - 180, position.Y + (size.Y / 2) - 70);
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
