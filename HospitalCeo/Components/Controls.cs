using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Console;

/*
 * Brett Taylor
 * All input should go through this class.
 */

namespace HospitalCeo.Components
{
    public class Controls : Component, IUpdatable     
    {
        public static Camera CAMERA { get; private set; }
        private const int WORLD_WIDTH = World.WorldController.WORLD_WIDTH * 100 - 50, WORLD_HEIGHT = World.WorldController.WORLD_HEIGHT * 100 - 50, MARGIN = 5;
        private static Vector2 cameraOrigin, cursorDifference, currentFramePosition, anchoredCameraTopLeft, anchoredCameraBottomRight;
        private static bool isDragging;

        public Controls() : base()
        {
            CAMERA = World.WorldController.SCENE.camera;
        }

        public void update()
        {
            if (DebugConsole.instance.IsOpen()) return;
            currentFramePosition = CAMERA.screenToWorldPoint(Input.mousePosition);
            anchoredCameraTopLeft = CAMERA.screenToWorldPoint(new Point(0, 0));
            anchoredCameraBottomRight = CAMERA.screenToWorldPoint(new Point(Screen.width, Screen.height));

            // ------------ Keyboard Camera Movement ------------
            if (IsKeyDown(Keys.W) || IsKeyDown(Keys.Up))
            {
                if (anchoredCameraTopLeft.Y <= -MARGIN) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X, CAMERA.transform.position.Y - 10);
            }

            if (IsKeyDown(Keys.S) || IsKeyDown(Keys.Down))
            {
                if (anchoredCameraBottomRight.Y >= WORLD_HEIGHT) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X, CAMERA.transform.position.Y + 10f);
            }

            if (IsKeyDown(Keys.A) || IsKeyDown(Keys.Left))
            {
                if (anchoredCameraTopLeft.X <= -MARGIN) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X - 10f, CAMERA.transform.position.Y);
            }

            if (IsKeyDown(Keys.D) || IsKeyDown(Keys.Right))
            {
                if (anchoredCameraBottomRight.X >= WORLD_WIDTH) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X + 10f, CAMERA.transform.position.Y);
            }

            // ------------ Mouse Camera Movement ------------
            if (IsMiddleMouseDown())
            {
                cursorDifference = (CAMERA.screenToWorldPoint(Input.mousePosition)) - CAMERA.transform.position;
                if (isDragging == false)
                {
                    isDragging = true;
                    cameraOrigin = CAMERA.screenToWorldPoint(Input.mousePosition);
                }
            }
            else
                isDragging = false;

            if (isDragging == true)
            {
                CAMERA.transform.position = cameraOrigin - cursorDifference;
                IsOutOfBoundsFromDrag(anchoredCameraTopLeft, anchoredCameraBottomRight);
            }

            // ------------ Camera Zoom ------------
            if (Input.mouseWheelDelta < 0)
            {
                if (CAMERA.zoom > -0.7)
                {
                    CAMERA.zoom -= 0.1f;
                    IsOutOfBoundsFromDrag(anchoredCameraTopLeft, anchoredCameraBottomRight);
                }
            }

            if (Input.mouseWheelDelta > 0)
            {
                if (CAMERA.zoom < -0.2)
                {
                    CAMERA.zoom += 0.1f;
                    IsOutOfBoundsFromDrag(anchoredCameraTopLeft, anchoredCameraBottomRight);
                }
            }

            // ------------ Edge Scrolling ------------
            if (Input.mousePosition.X < 10)
            {
                if (anchoredCameraTopLeft.X <= -MARGIN) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X - 10f, CAMERA.transform.position.Y);
            }

            if (Input.mousePosition.X > Screen.width - 10)
            {
                if (anchoredCameraBottomRight.X >= WORLD_WIDTH) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X + 10f, CAMERA.transform.position.Y);
            }

            if (Input.mousePosition.Y < 50)
            {
                if (anchoredCameraTopLeft.Y <= WORLD_HEIGHT) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X, CAMERA.transform.position.Y - 10f);
            }

            if (Input.mousePosition.Y > Screen.height - 10)
            {
                if (anchoredCameraBottomRight.Y >= WORLD_HEIGHT) return;
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X, CAMERA.transform.position.Y + 10f);
            }
        }

        private void IsOutOfBoundsFromDrag(Vector2 topLeft, Vector2 bottomRight)
        {
            if (CAMERA.transform.position.Y >= WORLD_HEIGHT - (Screen.height / 2))
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X, WORLD_HEIGHT + MARGIN - (Screen.height / 2));

            if (CAMERA.transform.position.Y <= -MARGIN + (Screen.height/ 2))
                CAMERA.transform.position = new Vector2(CAMERA.transform.position.X, -MARGIN + (Screen.height / 2));

            if (CAMERA.transform.position.X <= -MARGIN + (Screen.width / 2))
                CAMERA.transform.position = new Vector2( -MARGIN + (Screen.width / 2), CAMERA.transform.position.Y);

            if (CAMERA.transform.position.X >= WORLD_WIDTH - (Screen.width / 2))
                CAMERA.transform.position = new Vector2( WORLD_WIDTH + MARGIN - (Screen.width / 2), CAMERA.transform.position.Y);
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
            return Mouse.GetState().MiddleButton == ButtonState.Pressed? true: false;
        }

        public static Vector2 GetScreenWorldPoint()
        {
            return currentFramePosition;
        }
    }
}
  