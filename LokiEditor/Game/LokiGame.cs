using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Scene;
using Loki2D.Systems;
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

        SpriteBatch spriteBatch;

        protected override void Initialize()
        {

            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _keyboard = new WpfKeyboard(this);
            _mouse = new WpfMouse(this);

            SceneManagement.Instance = new SceneManagement(GraphicsDevice);
            TextureManager.Instance = new TextureManager(GraphicsDevice);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.StartCapture();

            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();


            SceneManagement.Instance.Update(gameTime);


            InputManager.EndCapture();
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManagement.Instance.Draw(spriteBatch);
        }
    }
}
