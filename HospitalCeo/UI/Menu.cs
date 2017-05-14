using Nez;
using Nez.UI;
using System;

namespace HospitalCeo.UI
{
    public abstract class Menu
    {
        protected Entity entity;
        protected UICanvas canvas;
        protected Table table;
        protected Skin defaultSkin;

        public bool enabled => throw new NotImplementedException();

        public int updateOrder => throw new NotImplementedException();

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
            OnDestory();

            OutOfGameScreenMenu scene = this as OutOfGameScreenMenu;
            if (scene != null)
                scene.DestoryScene();
            else
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

        protected virtual void OnDestory()
        {
        }

        public virtual void Update()
        {

        }

        protected abstract Entity CreateEntity();
        protected abstract UICanvas CreateUICanvas();
        protected abstract Table CreateTable();
    }
}
