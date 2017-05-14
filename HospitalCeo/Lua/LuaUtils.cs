using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;
using Nez;
using System.Timers;

namespace HospitalCeo.Lua
{
    [MoonSharpUserData]
    public static class LuaUtils
    {
        public static void Log(string log)
        {
            Nez.Console.DebugConsole.instance.log(log);
            Nez.Console.DebugConsole.instance.Open();
        }

        public static void HiddenLog(string log)
        {
            Nez.Console.DebugConsole.instance.log(log);
        }

        public static void Timer(string func, double ms)
        {
            System.Timers.Timer sttime = new System.Timers.Timer(ms);
            sttime.Start();
            sttime.Elapsed += delegate (object sender, ElapsedEventArgs e) {
                LuaManager.luaScript.Call(LuaManager.luaScript.Globals[func]);
                sttime.Stop();
                sttime = null;
            };
        }
    }
}
