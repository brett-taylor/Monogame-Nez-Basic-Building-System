using Microsoft.Xna.Framework;
using Nez.UI;

namespace HospitalCeo.UI.Elements.UI_GAME
{
    public class ZoneBuildMenu : InGameScreenMenu
    {
        private TextButton office;

        protected override void AfterCreation()
        {
            AddButton("Office", out office);

            office.onClicked += onClicked =>
            {
                Zoning.ZoneController.StartZoning(typeof(Zoning.Office));
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
            t.padLeft(150);
            t.defaults().setPadLeft(10).setPadBottom(5).setMinWidth(140).setMinHeight(30);
            t.setFillParent(true).bottom().left();
            t.setColor(Color.Red);
            return t;
        }
    }
}
