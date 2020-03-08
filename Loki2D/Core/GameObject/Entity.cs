using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.GameObject
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanUpdate { get; set; }

        internal List<Component.Component> Components = new List<Component.Component>();

        public Entity()
        {
            CanUpdate = true;
        }

        public Entity(int id, string name)
        {
            Id = id;
            Name = name; 
        }

        public T GetComponent<T>() where T: Component.Component
        {
            return (T)Components.First(x => x.GetType() == typeof(T));
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var components in Components)
            {
                components.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in Components)
            {
                if (component.CanDraw)
                {
                    component.Draw(spriteBatch);
                }
            }
        }
    }
}
