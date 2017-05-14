using HospitalCeo.World;
using Nez;

namespace HospitalCeo.UI
{
    public abstract class InGameScreenMenu : Menu
    {
        protected override Entity CreateEntity()
        {
            return WorldController.SCENE.createEntity("In Game Menu Holder");
        }

        protected override UICanvas CreateUICanvas()
        {
            canvas = new UICanvas();
            canvas.isFullScreen = true;
            canvas.setRenderLayer(WorldController.SCREEN_SPACE_RENDER_LAYER);
            return canvas;
        }
    }
}
