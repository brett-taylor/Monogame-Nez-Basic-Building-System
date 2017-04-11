using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace HospitalCeo.UI.Elements.UI_GAME
{
    public class StaffBuildMenu : InGameScreenMenu
    {
        private TextButton worker;

        protected override void AfterCreation()
        {
            AddButton("Worker", out worker);
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
            t.padLeft(360);
            t.defaults().setPadLeft(10).setPadBottom(5).setMinWidth(170).setMinHeight(30);
            t.setFillParent(true).bottom().left();
            t.setColor(Color.Red);
            return t;
        }
    }
}
