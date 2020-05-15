using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using tainicom.Aether.Physics2D.Dynamics;

namespace Loki2D.GUI
{
    /// <summary>
    /// Base class for UI Elements
    /// </summary>
    public class UIElement
    {
        /// <summary>
        /// LightColor Tint
        /// </summary>
        public Color Color { get; set; }
        
        /// <summary>
        /// Element ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Enables updates
        /// </summary>
        public bool CanUpdate { get; set; }
        
        /// <summary>
        /// Enables Drawing
        /// </summary>
        public bool CanDraw { get; set; }

        /// <summary>
        /// Element Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Element Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Element Scale X
        /// </summary>
        public float ScaleX { get; set; } = 1;

        /// <summary>
        /// Element Scale Y
        /// </summary>
        public float ScaleY { get; set; } = 1;

        /// <summary>
        /// Position of Element
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Parent UI Element
        /// </summary>
        public UIElement Parent;

        /// <summary>
        /// Disables drawing
        /// </summary>
        public void Hide()
        {
            CanDraw = false;
        }
       
        /// <summary>
        /// Enables drawing
        /// </summary>
        public void Show()
        {
            CanDraw = true;
        }

        /// <summary>
        /// Disables Updates
        /// </summary>
        public void DisableUpdate()
        {
            CanUpdate = false;
        }

        /// <summary>
        /// Enables updates
        /// </summary>
        public void EnableUpdate()
        {
            CanUpdate = true;
        }

        /// <summary>
        /// Enables the element
        /// </summary>
        public void Enable()
        {
            Show();
            EnableUpdate();
        }

        /// <summary>
        /// Disables the element
        /// </summary>
        public void Disable()
        {
            Hide();
            DisableUpdate();
        }

        /// <summary>
        /// Sets the tint color
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            Color = color;
        }

        /// <summary>
        /// Sets the element position
        /// </summary>
        /// <param name="point"></param>
        public void SetPosition(Point point)
        {
            Position = point; 
        }

        /// <summary>
        /// sets the parent 
        /// </summary>
        /// <param name="element"></param>
        public void SetParent(UIElement element)
        {
            Parent = element;
        }

        /// <summary>
        /// Clears the parent element
        /// </summary>
        /// <param name="element"></param>
        public void ClearParent(UIElement element)
        {
            Parent = null;
        }
    }
}
