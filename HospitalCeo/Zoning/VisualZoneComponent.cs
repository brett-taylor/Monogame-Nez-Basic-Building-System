using Microsoft.Xna.Framework;
using Nez;

namespace HospitalCeo.Zoning
{
    public class VisualZoneComponent : RenderableComponent
    {
        private Zone zone;

        public override float width {
            get {
                return zone != null ? zone.GetSize().X * 100 - 40 : 60;
            }
        }

        public override float height
        {
            get
            {
                return zone != null ? zone.GetSize().Y * 100 - 40 : 60;
            }
        }

        public VisualZoneComponent(Zone zone)
        {
            this.zone = zone;
            renderLayer = Utils.RenderLayers.ZONES;
        }

        public override void render(Graphics graphics, Camera camera)
        {
            foreach (World.Tile t in zone.GetTiles())
            {
                graphics.batcher.drawHollowRect(new Rectangle((int) t.GetPosition().X - 50, (int) t.GetPosition().Y - 50, ((int) 100), ((int) 100)), new Color(zone.GetColor(), 0.5f), thickness: 6);
            }
        }
    }
}
