using Microsoft.Xna.Framework;
using Nez.UI;

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
                Building.BuildingController.StartBuilding(new Building.Infrastructure.ConcreteWall());
                Hide();
            };

            cheapConcreteFlooring.onClicked += onClicked =>
            {
                Building.BuildingController.StartBuilding(new Building.Infrastructure.ConcreteFloorOne());
                Hide();
            };

            cheapCarpetFlooring.onClicked += onClicked =>
            {
                Building.BuildingController.StartBuilding(new Building.Infrastructure.CarpetFloorOne());
                Hide();
            };

            concreteFoundation.onClicked += onClicked =>
            {
                Building.BuildingController.StartBuilding(new Building.Infrastructure.ConcreteWallFoundation());
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
            t.defaults().setPadLeft(10).setPadBottom(5).setMinWidth(140).setMinHeight(30);
            t.setFillParent(true).bottom().left();
            t.setColor(Color.Red);
            return t;
        }
    }
}
