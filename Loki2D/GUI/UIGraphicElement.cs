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
            GraphicPath = path;
            var texture = TextureManager.Instance.GetTexture(GraphicPath);
            Width = texture.Width;
            Height = texture.Height; 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(TextureManager.Instance.GetTexture(GraphicPath), new Rectangle(GetPosition(), 
                new Point((int)(Width * ScaleX), (int)(Height * ScaleY))), Color.White);

            // draw children
            foreach (var child in Children)
            {
                child.Draw(spriteBatch);
            }

        }
    }
}
