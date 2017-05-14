using MoonSharp.Interpreter;
using System;
using System.IO;

namespace HospitalCeo.Lua
{
    public static class LuaManager
    {
        public static Script luaScript { get; private set; }

        public static void Initialise()
        {
            if (luaScript != null) return;
            UserData.RegisterAssembly();
            luaScript = new Script();

            luaScript.Globals["Data.Building"] = typeof(Building.Building);
            luaScript.Globals["Data.Workman"] = typeof(AI.Staff.Workman);
            luaScript.Globals["Data.Task"] = typeof(Tasks.Task);
            luaScript.Globals["Data.Subtask"] = typeof(Tasks.Subtask);
            luaScript.Globals["Data.Process"] = typeof(Tasks.Process);
            luaScript.Globals["Data.Instruction"] = new Tasks.Instruction();
            luaScript.Globals["Data.Tile"] = typeof(World.Tile);

            luaScript.Globals["Building"] = typeof(LuaBuildings);
            luaScript.Globals["Mob"] = typeof(LuaMob);
            luaScript.Globals["Util"] = typeof(LuaUtils);
            luaScript.Globals["World"] = typeof(LuaWorld);
            luaScript.Globals["Task"] = typeof(LuaTasks);
        }

        public static void LoadAllFilesInLuaDirectory()
        {
            Initialise();
            foreach (string directory in Directory.GetFiles("Content/hospitalceo/lua/", "*.lua", SearchOption.AllDirectories))
            {
                LoadLuaFile(directory);
            }
        }

        public static void LoadLuaFile(string directory)
        {
            Initialise();
            try
            {
                luaScript.DoFile(directory);
            }
            catch(Exception exception)
            {
                Nez.Console.DebugConsole.instance.log("LoadLuaFile -> Lua Error");
                Nez.Console.DebugConsole.instance.log(exception);
                Nez.Console.DebugConsole.instance.Open();
            }
        }

        public static void LoadLuaCode(string luaCode)
        {
            Initialise();
            try
            {
                luaScript.DoString(luaCode);
            }
            catch (Exception exception)
            {
                Nez.Console.DebugConsole.instance.log("LoadLuaCode -> Lua Error");
                Nez.Console.DebugConsole.instance.log(exception);
                Nez.Console.DebugConsole.instance.Open();
            }
        }
    }
}
