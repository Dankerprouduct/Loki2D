using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Sets the display text.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            Text = text;
        }
    }
}
