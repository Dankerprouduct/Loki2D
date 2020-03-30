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

        /// <summary>
        /// checks if a component exists in the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasComponent<T>() where T :Component.Component
        {
            foreach (var component in Components)
            {
                return component is T;
            }

            return false;
        }

        /// <summary>
        /// returns the first component of the same type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T: Component.Component
        {
            return (T)Components.First(x => x.GetType() == typeof(T));
        }

        /// <summary>
        /// Adds a component to the the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public T AddComponent<T>(T component) where T : Component.Component
        {
            Components.Add(component);
            return component;
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
