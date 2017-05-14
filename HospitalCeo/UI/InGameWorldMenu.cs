using HospitalCeo.World;
using Nez;

namespace HospitalCeo.UI
{
    public abstract class InGameWorldMenu : Menu
    {
        protected override Entity CreateEntity()
        {
            return WorldController.SCENE.createEntity("In Game Menu Holder");
        }

        protected override UICanvas CreateUICanvas()
        {
            canvas = new UICanvas();
            canvas.setRenderLayer(0);
            return canvas;
        }
    }
}
