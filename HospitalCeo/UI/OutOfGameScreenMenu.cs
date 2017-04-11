using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez.UI;
using Nez;
using HospitalCeo.World;
using Microsoft.Xna.Framework.Graphics;

namespace HospitalCeo.UI
{
    public abstract class OutOfGameScreenMenu : Menu
    {
        protected Scene scene;

        protected override void BeforeCreation()
        {
            scene = Scene.createWithDefaultRenderer(GetSceneColor());
            Core.scene = scene;
        }

        protected override Entity CreateEntity()
        {
            return scene.createEntity("Out Of Game Menu Holder");
        }

        protected override UICanvas CreateUICanvas()
        {
            canvas = new UICanvas();
            canvas.isFullScreen = true;
            return canvas;
        }

        protected abstract Color GetSceneColor();
    }
}
