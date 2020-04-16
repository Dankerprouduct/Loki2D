using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Utilities
{
    public static class Debug
    {
        public static EventHandler<LogEvent> Logged;

        public struct DebugRectangle
        {
            public Rectangle Rectangle { get; set; }
            public Color Color { get; set; }
        }
        private static List<DebugRectangle> _rectList = new List<DebugRectangle>();
        

        public static void Log(object log)
        {
            var logEvent = new LogEvent() {Log = log};
            Logged?.Invoke(null, logEvent);

            Console.WriteLine(log);
        }

        public static void DrawRect(DebugRectangle rectangle)
        {
            _rectList.Add(rectangle);
        }

        public static void DrawDebug(SpriteBatch spriteBatch)
        {
            var graphicsDevice = SceneManagement.Instance.CurrentScene.GraphicsDevice;
            var pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[1] { Color.White });

            for (int i = 0; i < _rectList.Count; i++)
            {
                spriteBatch.Draw(pixel, _rectList[i].Rectangle, _rectList[i].Color);
                _rectList.RemoveAt(i);
            }
        }
    }

    public class LogEvent : EventArgs
    {
        public object Log;
    }
}
