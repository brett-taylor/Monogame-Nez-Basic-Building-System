using Microsoft.Xna.Framework;
using Nez;

namespace HospitalCeo.Building.Shared_Components
{
    public class ConstructionRenderer : RenderableComponent
    {
        private Vector2 pos;
        private float percentage = 0f;
        private float constructionWidth = 100f, constructionHeight = 100f;

        public override RectangleF bounds
        {
            get
            {
                return new RectangleF(pos, new Vector2(constructionWidth, 100f));
            }
        }

        public ConstructionRenderer(Vector2 position)
        {
            this.pos = position;
            renderLayer = Utils.RenderLayers.CONSTRUCTION;
        }

        public ConstructionRenderer()
        {
            renderLayer = Utils.RenderLayers.CONSTRUCTION;
        }

        public void SetPercentage(float percentage)
        {
            this.percentage = percentage;
        }

        public override void render(Graphics graphics, Camera camera)
        {
            graphics.batcher.drawHollowRect(new Rectangle((int) pos.X + 10, (int) pos.Y + 10, (int) constructionWidth - 20, (int) constructionHeight - 20), Color.White, thickness: 5);
            graphics.batcher.drawRect(new Rectangle((int) pos.X + 10, (int) pos.Y + 10 + (80 - (int) percentage), (int) constructionWidth - 20, (int) percentage), new Color(Color.Green, 0.3f));
        }

        public void SetWidth(float width)
        {
            this.constructionWidth = width;
        }

        public void SetHeight(float height)
        {
            this.constructionHeight = height;
        }

        public override void onRemovedFromEntity()
        {
            base.onRemovedFromEntity();
        }
    }
}
