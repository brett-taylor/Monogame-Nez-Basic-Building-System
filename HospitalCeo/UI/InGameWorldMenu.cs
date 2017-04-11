using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez.UI;
using Nez;
using HospitalCeo.World;
using Microsoft.Xna.Framework.Graphics;

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
