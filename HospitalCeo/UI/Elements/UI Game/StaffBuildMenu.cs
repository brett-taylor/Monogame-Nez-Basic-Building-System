using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;

namespace HospitalCeo.UI.Elements.UI_GAME
{
    public class StaffBuildMenu : InGameScreenMenu
    {
        private TextButton worker;

        protected override void AfterCreation()
        {
            AddButton("Worker", out worker);

            worker.onClicked += onClicked =>
            {
                Entity e = WorldController.SCENE.createEntity("Worker");
                e.addComponent(new AI.Staff.Workman());
                e.addComponent(new Appearance.WorkerAppearence());
                e.position = new Vector2(Random.range(300, 700), Random.range(300, 700));

                Hide();
            };
        }

        private void AddButton(string text, out TextButton button)
        {
            TextButton b = new TextButton(text, defaultSkin);
            table.add(b);
            table.row();
            button = b;
        }

        protected override Table CreateTable()
        {
            Table t = new Table();
            t.padBottom(40);
            t.padLeft(450);
            t.defaults().setPadLeft(10).setPadBottom(5).setMinWidth(140).setMinHeight(30);
            t.setFillParent(true).bottom().left();
            t.setColor(Color.Red);
            return t;
        }
    }
}
