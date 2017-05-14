using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;
using Nez;

namespace HospitalCeo.Lua
{
    [MoonSharpUserData]
    public static class LuaMob
    {
        public static AI.Staff.Workman CreateWorker(int positionX, int positionY)
        {
            Entity entity = World.WorldController.SCENE.createEntity("Worker");
            AI.Staff.Workman worker = entity.addComponent(new AI.Staff.Workman());
            entity.addComponent(new Appearance.WorkerAppearence());
            entity.position = new Vector2(positionX, positionY);

            return worker;
        }
    }
}
