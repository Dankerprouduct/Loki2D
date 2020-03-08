using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Loki2D.Systems
{
    public static class InputManager
    {
        public static KeyboardState KeyboardState { get; set; }
        public static KeyboardState OldKeyboardState { get; set; }

        public static void StartCapture()
        {
            KeyboardState = Keyboard.GetState();
        }

        public static void EndCapture()
        {
            OldKeyboardState = KeyboardState;
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && OldKeyboardState.IsKeyUp(key);
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
}
