using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace HospitalCeo.UI.Elements.UI_GAME
{
    public class InfrastructureBuildMenu : InGameScreenMenu
    {
        private TextButton concreteWall, cheapConcreteFlooring, cheapCarpetFlooring, concreteFoundation;

        protected override void AfterCreation()
        {
            AddButton("Concrete Wall", out concreteWall);
            AddButton("Cheap Concrete Flooring", out cheapConcreteFlooring);
            AddButton("Cheap Carpet Flooring", out cheapCarpetFlooring);
            AddButton("Concrete Foundation", out concreteFoundation);

            concreteWall.onClicked += onClicked =>
            {
                Building.BuildingController.StartBuilding(typeof(Building.CheapWall));
                Hide();
            };

            cheapConcreteFlooring.onClicked += onClicked =>
            {
                Building.BuildingController.StartBuilding(typeof(Building.FlooringCheapConcrete));
                Hide();
            };

            cheapCarpetFlooring.onClicked += onClicked =>
            {
                Building.BuildingController.StartBuilding(typeof(Building.FlooringCheapCarpet));
                Hide();
            };

            concreteFoundation.onClicked += onClicked =>
            {
                Building.BuildingController.StartBuilding(typeof(Building.CheapWallFoundation));
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
            t.defaults().setPadLeft(10).setPadBottom(5).setMinWidth(170).setMinHeight(30);
            t.setFillParent(true).bottom().left();
            t.setColor(Color.Red);
            return t;
        }
    }
}
