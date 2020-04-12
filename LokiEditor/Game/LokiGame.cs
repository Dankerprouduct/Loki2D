using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Scene;
using Loki2D.Systems;
using LokiEditor.LokiControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;
using Debug = Loki2D.Core.Utilities.Debug;

namespace LokiEditor.Game
{
    public class LokiGame : WpfGame
    {
        private IGraphicsDeviceService _graphicsDeviceManager;
        private WpfKeyboard _keyboard;
        private WpfMouse _mouse;
        public static LokiGame Instance;

        SpriteBatch spriteBatch;

        protected override void Initialize()
        {
            Instance = this; 
            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _keyboard = new WpfKeyboard(this);
            _mouse = new WpfMouse(this);

            SceneManagement.Instance = new SceneManagement(GraphicsDevice);
            TextureManager.Instance = new TextureManager(GraphicsDevice);

            base.Initialize();
        }

        public void LoadScene(string filePath, Assembly assembly)
        {
            SceneManagement.Instance.LoadScene(filePath, assembly);
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.MouseState = _mouse.GetState();
            InputManager.KeyboardState = _keyboard.GetState();

            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();

            if (InputManager.LeftMouseClicked())
            {
                Debug.Log($"clicked mouse {DateTime.Now}");
                if (SceneView.CurrentEditType == SceneView.EditType.Brush)
                {
                    var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                        Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));
                    if (AssetControl.Instance.SelectedAsset != null)
                    {
                        
                    }
                }
            }

            SceneManagement.Instance.Update(gameTime);
            SceneManagement.Instance.CurrentScene?.Camera.WSADMovement();
            InputManager.EndCapture();
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManagement.Instance.Draw(spriteBatch);
        }
    }
}
