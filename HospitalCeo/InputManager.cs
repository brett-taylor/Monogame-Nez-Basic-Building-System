using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Console;

/*
 * Brett Taylor
 * All input should go through this class.
 */

namespace HospitalCeo
{
    public static class InputManager
    {
        public static Camera camera { get; private set; }
        private const int WORLD_WIDTH = World.WorldController.WORLD_WIDTH * 100 - 50, WORLD_HEIGHT = World.WorldController.WORLD_HEIGHT * 100 - 50, MARGIN = 5;
        private static Vector2 cameraOrigin, cursorDifference, currentFramePosition;
        private static bool isDragging;

        public static void Initialise()
        {
            // Set up the camera stuff
            camera = World.WorldController.SCENE.camera;
            camera.setMaximumZoom(1.5f);
            camera.setMinimumZoom(0.35f);
        }

        public static void Update()
        {
            if (DebugConsole.instance.IsOpen()) return;
            currentFramePosition = camera.screenToWorldPoint(Input.mousePosition);

            // ------------ Keyboard Camera Movement ------------
            if (IsKeyDown(Keys.W) || IsKeyDown(Keys.Up))
            {
                if (camera.position.Y < 0) return;
                camera.transform.position = new Vector2(camera.transform.position.X, camera.transform.position.Y - 10);
            }

            if (IsKeyDown(Keys.S) || IsKeyDown(Keys.Down))
            {
                if (camera.position.Y > WORLD_HEIGHT) return;
                camera.transform.position = new Vector2(camera.transform.position.X, camera.transform.position.Y + 10f);
            }

            if (IsKeyDown(Keys.A) || IsKeyDown(Keys.Left))
            {
                if (camera.position.X < 0) return;
                camera.transform.position = new Vector2(camera.transform.position.X - 10f, camera.transform.position.Y);
            }

            if (IsKeyDown(Keys.D) || IsKeyDown(Keys.Right))
            {
                if (camera.position.X > WORLD_WIDTH) return;
                camera.transform.position = new Vector2(camera.transform.position.X + 10f, camera.transform.position.Y);
            }

            // ------------ Mouse Camera Movement ------------
            if (IsMiddleMouseDown())
            {
                cursorDifference = (camera.screenToWorldPoint(Input.mousePosition)) - camera.transform.position;
                if (isDragging == false)
                {
                    isDragging = true;
                    cameraOrigin = camera.screenToWorldPoint(Input.mousePosition);
                }
            }
            else
                isDragging = false;

            if (isDragging == true)
            {
                camera.transform.position = cameraOrigin - cursorDifference;
                OutOfBounds();
            }

            // ------------ Camera Zoom ------------
            if (Input.mouseWheelDelta < 0 || IsKeyDown(Keys.Subtract) || IsKeyDown(Keys.OemMinus))
            {
                camera.zoomOut(0.1f);
                OutOfBounds();
            }

            if (Input.mouseWheelDelta > 0 || IsKeyDown(Keys.Add) || IsKeyDown(Keys.OemPlus))
            {
                camera.zoomIn(0.1f);
                OutOfBounds();
            }

            // ------------ Edge Scrolling ------------
            if (Input.mousePosition.X < 10 && camera.position.X > 0)
            {
                camera.transform.position = new Vector2(camera.transform.position.X - 10f, camera.transform.position.Y);
            }

            if (Input.mousePosition.X > Screen.width - 10 && camera.position.X < WORLD_WIDTH)
            {
                camera.transform.position = new Vector2(camera.transform.position.X + 10f, camera.transform.position.Y);
            }

            if (Input.mousePosition.Y < 10 && camera.position.Y > 0)
            {
                camera.transform.position = new Vector2(camera.transform.position.X, camera.transform.position.Y - 10f);
            }

            if (Input.mousePosition.Y > Screen.height - 10 && camera.position.Y < WORLD_HEIGHT)
            {
                camera.transform.position = new Vector2(camera.transform.position.X, camera.transform.position.Y + 10f);
            }
        }

        private static void OutOfBounds()
        {
            if (camera.position.Y < 0)
                camera.transform.position = new Vector2(camera.transform.position.X, 0);

            if (camera.position.Y > WORLD_HEIGHT)
                camera.transform.position = new Vector2(camera.transform.position.X, WORLD_HEIGHT);

            if (camera.position.X < 0)
                camera.transform.position = new Vector2(0, camera.transform.position.Y);

            if (camera.position.X > WORLD_WIDTH)
                camera.transform.position = new Vector2(WORLD_WIDTH, camera.transform.position.Y);
        }

        public static bool IsKeyDown(Keys kc)
        {
            return Input.isKeyDown(kc);
        }

        public static bool IsLeftMouseDown()
        {
            return Input.leftMouseButtonDown;
        }

        public static bool IsRightMouseDown()
        {
            return Input.rightMouseButtonDown;
        }

        public static bool IsMiddleMouseDown()
        {
            return Mouse.GetState().MiddleButton == ButtonState.Pressed ? true : false;
        }

        public static Vector2 GetScreenWorldPoint()
        {
            return currentFramePosition;
        }
    }
}