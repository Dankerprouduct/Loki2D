using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Scene;
using Loki2D.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Systems
{
    public class GUIManager: SystemManager<GUIManager>
    {
        /// <summary>
        /// Holds elements displayed on screen
        /// </summary>
        private List<UIElement> _uiElements;

        private List<UIElement> _elementsToAdd;
        private List<UIElement> _elementsToRemove;

        public static int ScreenWidth = SceneManagement.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
        public static int ScreenHeight = SceneManagement.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight;

        public GUIManager()
        {
            _uiElements = new List<UIElement>();
            _elementsToAdd = new List<UIElement>();
            _elementsToRemove = new List<UIElement>();
        }

        /// <summary>
        /// Adds UI Element to be rendered and updated
        /// </summary>
        /// <param name="uiElement"></param>
        public void AddElement(UIElement uiElement)
        {
            _elementsToAdd.Add(uiElement);
        }

        /// <summary>
        /// Removes UI Element
        /// </summary>
        /// <param name="uiElement"></param>
        public void RemoveElement(UIElement uiElement)
        {
            if (_uiElements.Contains(uiElement))
            {
                _elementsToRemove.Add(uiElement);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //adding new elements;
            for (int i = 0; i < _elementsToAdd.Count; i++)
            {
                var element = _elementsToAdd[i];
                _uiElements.Add(element);
                _elementsToAdd.Remove(element);
            }

            // removing queued elements
            for (int i = 0; i < _elementsToRemove.Count; i++)
            {
                var element = _elementsToRemove[i];
                _uiElements.Remove(element);
                _elementsToRemove.Remove(element);
            }


            foreach (var element in _uiElements)
            {
                if (element.CanUpdate)
                {
                    element.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var element in _uiElements)
            {
                if (element.CanDraw)
                {
                    element.Draw(spriteBatch);
                }
            }
            base.Draw(spriteBatch);
        }



    }
}
