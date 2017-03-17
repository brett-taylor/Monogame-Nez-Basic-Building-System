using Microsoft.Xna.Framework;
using Nez;
using Nez.Console;

/*
 * Brett Taylor
 * Commands for the console
 */

namespace HospitalCeo.Utils
{
    public class Commands
    {
        [Command("camera-pos", "Prints of the camera's position and zoom as well as the width and height of the screen.")]
        static void printCameraPosition()
        {
            DebugConsole.instance.log("Camera position: " + InputManager.camera.position + ", zoom: " + InputManager.camera.zoom + ", screen width: " + Screen.width + ", screen height: :" + Screen.height);
        }

        [Command("camera-reset", "Resets the camera to (0, 0)")]
        static void cameraReset()
        {
            InputManager.camera.position = new Vector2(0, 0);
        }

        [Command("building", "Starts Buidling")]
        static void startBuilding()
        {
            Building.InfrastructureBuildingController.StartBuilding(typeof(Building.BaseWall));
        }


        [Command("tile-under-cursor", "Prints the tile's position and the building on top of it. AS well as draws a box around it")]
        static void tileUnderCursor()
        {
            World.Tile t = World.WorldController.GetMouseOverTile();
            if (t == null)
            {
                DebugConsole.instance.log("No tile under cursor");
                return;
            }

            DebugConsole.instance.log(t);
            DebugConsole.instance.log("Infrastructure Building: " + t.GetInfrastructureItem());
            DebugConsole.instance.log("Gameplay Building: " + t.GetGameplayItem());
        }
    }
}
