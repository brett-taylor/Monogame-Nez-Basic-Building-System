using Microsoft.Xna.Framework;
using Nez.UI;

namespace HospitalCeo.UI.Elements.UI_World
{
    public class LoadingGameMenu : OutOfGameScreenMenu
    {
        private ProgressBar progressBar;

        protected override void AfterCreation()
        {
            progressBar = new ProgressBar(defaultSkin);
            table.add(progressBar).setMinWidth(300).setPrefWidth(800).setMaxWidth(1200);
            progressBar.setValue(0f);
        }

        protected override Table CreateTable()
        {
            Table t = new Table();
            t.padLeft(40);
            t.padRight(40);
            t.defaults().setPadTop(10).setMinWidth(170).setMinHeight(30);
            t.setFillParent(true).center();
            return t;
        }

        protected override Color GetSceneColor()
        {
            return new Color(38, 38, 38);
        }

        public override void Update()
        {
            base.Update();
            progressBar.setValue(GameStateManager.LOAD_PERCENT);
        }
    }
}
