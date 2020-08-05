using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.GUI
{
    public class UIGraphicElement: UIElement
    {
        /// <summary>
        /// The path for the graphic
        /// </summary>
        public string GraphicPath { get; set; }

        /// <summary>
        /// Sets the path for the graphic
        /// </summary>
        /// <param name="path"></param>
        public void SetPath(string path)
        {

            Console.WriteLine("Set path");
            GraphicPath = path;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Console.WriteLine("drawing element");
            spriteBatch.Draw(TextureManager.Instance.GetTexture(GraphicPath), GetPosition().ToVector2(), null, Color.White);

            // draw children
            foreach (var child in Children)
            {
                child.Draw(spriteBatch);
            }

        }
    }
}
