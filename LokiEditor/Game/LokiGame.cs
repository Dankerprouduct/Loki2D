using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;

namespace LokiEditor.Game
{
    public class LokiGame : WpfGame
    {
        private IGraphicsDeviceService _graphicsDeviceManager;
        private WpfKeyboard _keyboard;
        private WpfMouse _mouse;

        protected override void Initialize()
        {

            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);


            _keyboard = new WpfKeyboard(this);
            _mouse = new WpfMouse(this);
            base.Initialize();
        }

        protected override void Update(GameTime time)
        {
            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.Black);
        }
    }
}
