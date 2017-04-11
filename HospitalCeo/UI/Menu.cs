using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez.UI;
using Nez;
using HospitalCeo.World;
using Microsoft.Xna.Framework.Graphics;

namespace HospitalCeo.UI
{
    public abstract class Menu
    {
        protected Entity entity;
        protected UICanvas canvas;
        protected Table table;
        protected Skin defaultSkin;

        public Menu()
        {
            BeforeCreation();

            entity = CreateEntity();
            canvas = CreateUICanvas();
            table = CreateTable();

            entity.addComponent(canvas);
            canvas.stage.addElement(table);

            defaultSkin = Skin.createDefaultSkin();
            AfterCreation();
        }

        public void Show()
        {
            entity.setEnabled(true);
        }

        public void Hide()
        {
            entity.setEnabled(false);
        }

        public void Toggle()
        {
            entity.setEnabled(!entity.enabled);
        }

        public void Destory()
        {
            entity.destroy();
            entity = null;
            canvas = null;
            table = null;
        }

        protected virtual void BeforeCreation()
        {
        }

        protected virtual void AfterCreation()
        {
        }

        protected abstract Entity CreateEntity();
        protected abstract UICanvas CreateUICanvas();
        protected abstract Table CreateTable();
    }
}
