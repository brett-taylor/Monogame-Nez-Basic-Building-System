using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;
using Nez;

namespace HospitalCeo.Lua
{
    [MoonSharpUserData]
    public static class LuaBuildings
    {
        public static Building.Building Create(string name, int positionX, int positionY, int sizeX, int sizeY)
        {
            Building.PrimitiveBuilding primitive;
            switch (name.ToLower())
            {
                case "wall":
                    primitive = new Building.Infrastructure.ConcreteWall();
                    break;
                case "carpet":
                    primitive = new Building.Infrastructure.CarpetFloorOne();
                    break;
                case "floor":
                    primitive = new Building.Infrastructure.ConcreteFloorOne();
                    break;
                default:
                    Nez.Console.DebugConsole.instance.log("Lua is trying to load a object that doesnt exist: " + name);
                    return null;
            }

            Entity entity = World.WorldController.SCENE.createEntity("Building at x: " + positionX + " y: " + positionY);
            Building.Building building = new Building.Building();
            entity.addComponent(building);
            building.SetPrimitiveBuilding(primitive);
            building.SetInfo(new Vector2(positionX, positionY), new Vector2(sizeX, sizeY));

            return building;
        }
    }
}
