using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.GUI
{
    public class UIGraphicElement
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
        }
    }
}
