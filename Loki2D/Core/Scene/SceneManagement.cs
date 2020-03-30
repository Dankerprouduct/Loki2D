using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Scene
{
    public class SceneManagement
    {
        public static SceneManagement Instance;
        public Scene CurrentScene { get; private set; }

        public SceneManagement()
        {
            Instance = this; 
        }

        /// <summary>
        /// Loads a scene and sets the current scene
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(Scene scene)
        {
            CurrentScene = scene; 
        }


        /// <summary>
        /// Updates the current Scene;
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        /// <summary>
        /// Draws the current scene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);
        }
    }
}
