using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
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
        public Color Color { get; set; } = Color.White; 
        
        /// <summary>
        /// Element ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Enables updates
        /// </summary>
        public bool CanUpdate { get; set; } = true;

        /// <summary>
        /// Enables Drawing
        /// </summary>
        public bool CanDraw { get; set; } = true;

        /// <summary>
        /// Element Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Element Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The rotation of the element
        /// </summary>
        public float Rotation { get; set; } = 0;

        /// <summary>
        /// Element Scale X
        /// </summary>
        public float ScaleX { get; set; } = 1;

        /// <summary>
        /// Element Scale Y
        /// </summary>
        public float ScaleY { get; set; } = 1;

        public EventHandler<UIClickEventArgs> LeftClicked;

        public EventHandler<UIClickEventArgs> LeftPressed;
        public EventHandler<UIClickEventArgs> LeftReleased;

        public EventHandler<UIClickEventArgs> RightClicked;

        public EventHandler<UIClickEventArgs> RightReleased;

        /// <summary>
        /// Position of Element
        /// </summary>
        public Point Position { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Parent UI Element
        /// </summary>
        public UIElement Parent;

        /// <summary>
        /// Children ui elements
        /// </summary>
        public List<UIElement> Children = new List<UIElement>();

        public Point GetPosition()
        {
            if (Parent == null)
                return Position;
            return Parent.GetPosition() + Position; 
        }

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
            Parent.Children.Add(this);
        }

        /// <summary>
        /// Clears the parent element
        /// </summary>
        /// <param name="element"></param>
        public void ClearParent(UIElement element)
        {
            Parent.Children.Remove(this);
            Parent = null;
        }

        /// <summary>
        /// Adds element as child object
        /// </summary>
        /// <param name="element"></param>
        public void AddChild(UIElement element)
        {
            element.SetParent(this);
            //Children.Add(element);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var child in Children)
            {
                child.Update(gameTime);
            }

            // no parent
            if (Parent == null)
            {
                var boundsRect = new Rectangle(Position, new Point(Width, Height));

                // left
                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.LeftMouseClicked())
                {
                    LeftClicked?.Invoke(this, new UIClickEventArgs(this));
                }

                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.LeftMouseReleased())
                {
                    LeftReleased?.Invoke(this, new UIClickEventArgs(this));
                }

                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.LeftMouseDown())
                {
                    LeftPressed?.Invoke(this, new UIClickEventArgs(this));
                }

                // right
                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.RightMouseClicked())
                {
                    RightClicked?.Invoke(this, new UIClickEventArgs(this));
                }

                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.RightMouseReleased())
                {
                    RightReleased?.Invoke(this, new UIClickEventArgs(this));
                }

            }
            else
            {
                var boundsRect = new Rectangle(GetPosition(), new Point(Width, Height));

                // left
                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.LeftMouseClicked())
                {
                    LeftClicked?.Invoke(this, new UIClickEventArgs(this));
                }

                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.LeftMouseReleased())
                {
                    LeftReleased?.Invoke(this, new UIClickEventArgs(this));
                }

                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.LeftMouseDown())
                {
                    LeftPressed?.Invoke(this, new UIClickEventArgs(this));
                }

                // right
                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.RightMouseClicked())
                {
                    RightClicked?.Invoke(this, new UIClickEventArgs(this));
                }

                if (boundsRect.Contains(InputManager.GetMousePosition().ToPoint()) &&
                    InputManager.RightMouseReleased())
                {
                    RightReleased?.Invoke(this, new UIClickEventArgs(this));
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Parent == null)
            {
                spriteBatch.Draw(TextureManager.Pixel, new Rectangle(GetPosition(), new Point(Width, Height)),
                    null, Color);
            }
            else
            {
                spriteBatch.Draw(TextureManager.Pixel, new Rectangle(GetPosition(), new Point(Width, Height)),
                    null, Color);
            }

            // draw children
            foreach (var child in Children)
            {
                child.Draw(spriteBatch);
            }

        }
    }
}
