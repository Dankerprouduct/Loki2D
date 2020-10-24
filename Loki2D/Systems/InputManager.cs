using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Loki2D.Systems
{
    public static class InputManager
    {
        public static KeyboardState KeyboardState { get; set; }
        public static KeyboardState OldKeyboardState { get; set; }
        public static MouseState MouseState { get; set; }
        public static MouseState OldMouseState { get; set; }


        public static void StartCapture()
        {
            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();
        }

        public static void EndCapture()
        {
            OldKeyboardState = KeyboardState;
            OldMouseState = MouseState; 
        }

        public static Vector2 GetMouseWorldPosition()
        {
            var position = Vector2.Transform(MouseState.Position.ToVector2(),
                Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));
            return position;
        }

        public static Vector2 GetMousePosition()
        {
            return MouseState.Position.ToVector2();
        }
        

        public static bool RightMouseClicked()
        {
            return MouseState.RightButton == ButtonState.Pressed &&
                   OldMouseState.RightButton == ButtonState.Released;
        }

        public static bool LeftMouseClicked()
        {
            return MouseState.LeftButton == ButtonState.Pressed &&
                   OldMouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftMouseDown()
        {
            return MouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftMouseUp()
        {
            return MouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftMouseReleased()
        {
            return MouseState.LeftButton == ButtonState.Released && OldMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool RightMouseReleased()
        {
            return MouseState.RightButton == ButtonState.Released && OldMouseState.RightButton == ButtonState.Pressed;
        }


        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && OldKeyboardState.IsKeyUp(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return (KeyboardState.IsKeyUp(key) && OldKeyboardState.IsKeyDown(key));
        }

        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        public static bool KeyUp(Keys key)
        {
            return KeyboardState.IsKeyUp(key);
        }

    }

    public class KeyEventArgs : EventArgs
    {
        public Keys KeyPressed { get; set; }
        public Keys KeyDown { get; set; }
        public Keys KeyUp { get; set; }
    }
}
