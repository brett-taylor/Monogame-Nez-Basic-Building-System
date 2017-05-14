using HospitalCeo.World;
using MoonSharp.Interpreter;

namespace HospitalCeo.Lua
{
    [MoonSharpUserData]
    public static class LuaWorld
    {
        public static Tile GetMouseOverTile()
        {
            return WorldController.GetMouseOverTile();
        }

        public static Tile GetTileAt(int x, int y)
        {
            return WorldController.GetTileAt(new Microsoft.Xna.Framework.Vector2(x, y), isTileCoords: true);
        }

        public static Tile GetTileAtWorldPosition(int x, int y)
        {
            return WorldController.GetTileAt(x, y);
        }
    }
}
