using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public GUIManager()
        {

        }

        /// <summary>
        /// Adds UI Element to be rendered and updated
        /// </summary>
        /// <param name="uiElement"></param>
        public void AddElement(UIElement uiElement)
        {
            _uiElements.Add(uiElement);
        }

        /// <summary>
        /// Removes UI Element
        /// </summary>
        /// <param name="uiElement"></param>
        public void RemoveElement(UIElement uiElement)
        {
            if (_uiElements.Contains(uiElement))
            {
                RemoveElement(uiElement);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var element in _uiElements)
            {
                if (element.CanUpdate)
                {

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

                }
            }
            base.Draw(spriteBatch);
        }



    }
}
