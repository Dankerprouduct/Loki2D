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
using LokiEditor.Actions;
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

        public static EventHandler<LoadedSceneArgs> SceneLoadedHandler; 
        public static EventHandler<EditedEntityArgs> EditedEntityHandler; 
        public static EventHandler<AddedEntityArgs> AddedEntityHandler; 

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
            SceneLoadedHandler?.Invoke(this, new LoadedSceneArgs()
            {
                CellSpacePartition = SceneManagement.Instance.CurrentScene.CellSpacePartition
            });
        }

        private Command command;
        protected override void Update(GameTime gameTime)
        {
            InputManager.MouseState = _mouse.GetState();
            InputManager.KeyboardState = _keyboard.GetState();

            if (SceneManagement.Instance.CurrentScene != null)
            {
                UpdateEditor();
            }

            SceneManagement.Instance.Update(gameTime);
            SceneManagement.Instance.CurrentScene?.Camera.WSADMovement();
            InputManager.EndCapture();
        }

        public void UpdateEditor()
        {
            if (InputManager.LeftMouseClicked() && command == null)
            {
                // Select
                if (SceneView.CurrentEditType == SceneView.EditType.Select)
                {
                    command = new SelectCommand();
                    command.Enter();
                }
            }

            if (InputManager.LeftMouseDown() && command == null)
            {
                // Brush
                if (SceneView.CurrentEditType == SceneView.EditType.Brush)
                {
                    command = new BrushCommand();
                    command.Enter();
                }

                // Transform
                if (SceneView.CurrentEditType == SceneView.EditType.Transform)
                {
                    command = new TransformCommand();
                    command.Enter();
                }

            }


            if (command != null)
            {
                if (!command.Update())
                {
                    command.Exit();
                    command = null;
                }
            }
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManagement.Instance.Draw(spriteBatch);
        }
    }

    public class LoadedSceneArgs : EventArgs
    {
        public CellSpacePartition CellSpacePartition { get; set; }
    }

    public class EditedEntityArgs : EventArgs
    {
        public Entity EditedEntity { get; set; }
    }

    public class AddedEntityArgs : EventArgs
    {
        public Entity AddedEntity { get; set; }
    }

    public class RemovedEntityArgs : EventArgs
    {
        public Entity RemovedEntity { get; set; }
    }
}
