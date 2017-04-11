using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace HospitalCeo.UI.Elements.UI_GAME
{
    public class ConstructionMenu : InGameScreenMenu
    {
        private TextButton infrastructure, zone, staff;

        protected override void AfterCreation()
        {
            AddButton("Infrastructure", out infrastructure);
            AddButton("Zone", out zone);
            AddButton("Staff", out staff);

            infrastructure.onClicked += onClicked =>
            {
                bool result = UIManager.Toggle("InfrastructureBuildMenu");
                if (result == false)
                    UIManager.Create("InfrastructureBuildMenu", new InfrastructureBuildMenu());
            };

            zone.onClicked += onClicked =>
            {
                bool result = UIManager.Toggle("ZoneBuildMenu");
                if (result == false)
                    UIManager.Create("ZoneBuildMenu", new ZoneBuildMenu());
            };

            staff.onClicked += onClicked =>
            {
                bool result = UIManager.Toggle("StaffBuildMenu");
                if (result == false)
                    UIManager.Create("StaffBuildMenu", new StaffBuildMenu());
            };
        }

        private void AddButton(string text, out TextButton button)
        {
            TextButton b = new TextButton(text, defaultSkin);
            table.add(b);
            button = b;
        }

        protected override Table CreateTable()
        {
            Table t = new Table();
            t.defaults().setPadLeft(10).setPadBottom(10).setMinWidth(170).setMinHeight(30);
            t.setFillParent(true).bottom().left();
            t.setColor(Color.Red);
            return t;
        }
    }
}
