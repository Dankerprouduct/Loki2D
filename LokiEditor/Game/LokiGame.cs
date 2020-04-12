using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using Loki2D.Systems;
using LokiEditor.LokiControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private Entity SelectedEntity;
        private bool Selected = false;
        protected override void Update(GameTime gameTime)
        {
            InputManager.MouseState = _mouse.GetState();
            InputManager.KeyboardState = _keyboard.GetState();

            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();

            if (InputManager.LeftMouseDown() && !Selected)
            {
                if (SceneView.CurrentEditType == SceneView.EditType.Brush)
                {
                    var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                        Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));
                    Debug.Log(position);
                    if (AssetControl.Instance.SelectedAsset != null)
                    {

                        var assembly = AssetControl.Instance.LoadedAssembly;
                        var type = assembly.GetType(AssetControl.Instance.SelectedAsset.FullType, true);
                        var entity = Activator.CreateInstance(type) as Entity;

                        entity.GetComponent<TransformComponent>().Position = position;
                        SelectedEntity = entity;
                        Selected = true;
                        SceneManagement.Instance.CurrentScene.AddEntity(entity);

                    }
                }
            }

            if (InputManager.LeftMouseDown() && Selected)
            {
                var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                    Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));

                var textureName = SelectedEntity.GetComponent<RenderComponent>().TextureName;
                var width = TextureManager.Instance.GetTexture(textureName).Width;
                var height = TextureManager.Instance.GetTexture(textureName).Height;

                if (InputManager.KeyDown(Keys.LeftShift))
                {
                    position.X = ((float) Math.Round(position.X / width) * width);
                    position.Y = ((float) Math.Round(position.Y / height) * height);
                }

                SelectedEntity.GetComponent<TransformComponent>().Position = position;
            }

            if (InputManager.LeftMouseUp() && Selected)
            {
                Selected = false; 
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
