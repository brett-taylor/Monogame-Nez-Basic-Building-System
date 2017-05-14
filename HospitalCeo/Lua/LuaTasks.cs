using HospitalCeo.AI.Staff;
using HospitalCeo.World;
using MoonSharp.Interpreter;

namespace HospitalCeo.Lua
{
    [MoonSharpUserData]
    public static class LuaTasks
    {
        public static void GiveWorkerInstruction(Workman worker, Tile tile, float time, string func)
        {
            Tasks.Instruction instruction = new Tasks.Instruction();
            Tasks.Process process = new Tasks.Process(tile, time, onreach => {
                LuaManager.luaScript.Call(LuaManager.luaScript.Globals[func], worker);
            });
            instruction.AddProcess(process);

            worker.SetCurrentInstruction(instruction);
        }

        public static void Clear()
        {
            Tasks.Task.Clear();
        }
    }
}
