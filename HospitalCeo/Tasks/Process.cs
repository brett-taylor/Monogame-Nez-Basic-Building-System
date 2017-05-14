using HospitalCeo.World;
using System;
using System.Collections.Generic;

/*
 * Brett Taylor
 * Action
 * This is the actual action that the task wants performed
 * E.G. Remove tree, Construct floor
 */

namespace HospitalCeo.Tasks
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class Process
    {
        private Tile tile;
        private float actionTime;
        private float remainingTime;
        private Instruction instruction;

        private Action<Process> onActionCompleted;
        private Action<Process> onActionTicked;
        private List<String> onActionCompletedLua;
        private List<String> onActionTickedLua;

        public Process(Tile tile, float actionTime, Action<Process> onActionCompleted)
        {
            this.tile = tile;
            this.actionTime = actionTime;
            this.remainingTime = actionTime;
            this.onActionCompleted += onActionCompleted;
        }

        public Process(Tile tile, float actionTime, string functionName)
        {
            this.tile = tile;
            this.actionTime = actionTime;
            this.remainingTime = actionTime;
            onActionCompletedLua = new List<string>();
            onActionTickedLua = new List<string>();
            onActionCompletedLua.Add(functionName);
        }

        public void RegisterOnCompleteHandle(Action<Process> callback)
        {
            onActionCompleted += callback;
        }

        public void RegisterOnTickHandle(Action<Process> callback)
        {
            onActionTicked += callback;
        }

        public void RegisterOnCompleteHandleLua(string functionName)
        {
            if (onActionCompletedLua == null) onActionCompletedLua = new List<string>();
            onActionCompletedLua.Add(functionName);
        }

        public void RegisterOnTickHandleLua(string functionName)
        {
            if (onActionTickedLua == null) onActionTickedLua = new List<string>();
            onActionTickedLua.Add(functionName);
        }

        public void DoProcess(float workAmount)
        {
            remainingTime -= workAmount;
            onActionTicked?.Invoke(this);
            if (onActionTickedLua != null)
                foreach (string s in onActionTickedLua)
                    Lua.LuaManager.luaScript.Call(Lua.LuaManager.luaScript.Globals[s]);

            if (remainingTime <= 0)
            {
                onActionCompleted?.Invoke(this);
                if (onActionCompletedLua != null)
                    foreach (string s in onActionCompletedLua)
                        Lua.LuaManager.luaScript.Call(Lua.LuaManager.luaScript.Globals[s]);
            }
        }

        public Tile GetWorkTile()
        {
            return tile;
        }

        public float GetProcessTime()
        {
            return actionTime;
        }

        public float GetRemainingTime()
        {
            return remainingTime;
        }

        public void SetInstruction(Instruction instruction)
        {
            this.instruction = instruction;
        }

        public Instruction GetInstruction()
        {
            return instruction;
        }
    }
}
