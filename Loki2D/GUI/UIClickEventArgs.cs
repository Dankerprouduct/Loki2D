using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.GUI
{
    public class UIClickEventArgs:EventArgs
    {
        /// <summary>
        /// The clicked element
        /// </summary>
        public UIElement Element;
        public UIClickEventArgs(UIElement element)
        {
            Element = element; 
        }
    }
}
