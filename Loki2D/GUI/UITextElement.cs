using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.GUI
{
    /// <summary>
    /// UI Text Element 
    /// </summary>
    public class UITextElement : UIElement
    {
        /// <summary>
        /// The text that will be displayed
        /// </summary>
        public string Text { get; set; }

        public SpriteFont Font { get; set; }

        /// <summary>
        /// Sets the display text.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            Text = text;
        }

        public void SetFont(SpriteFont font)
        {
            Font = font;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(Font == null)
                throw new Exception("No font set for UITextElement.");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Parent == null)
            {
                spriteBatch.DrawString(Font, Text, Position.ToVector2(), Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.DrawString(Font, Text, Position.ToVector2() + Parent.Position.ToVector2(), Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            // draw children
            foreach (var child in Children)
            {
                child.Draw(spriteBatch);
            }
        }
    }
}
