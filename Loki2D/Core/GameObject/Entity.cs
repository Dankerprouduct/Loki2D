using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Debug = Loki2D.Core.Utilities.Debug;

namespace Loki2D.Core.GameObject
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanUpdate { get; set; }
        public string EntityType { get; set; }
        public int Index;

        public Component.Component[] Components => _components?.ToArray();
        internal List<Component.Component> _components = new List<Component.Component>();
        
        public Entity()
        {
            CanUpdate = true;
            EntityType = GetType().FullName;
        }

        public Entity(int id, string name): this()
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
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(T))
                {
                    return true; 
                }
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
            return (T)_components.First(x => x.GetType() == typeof(T));
        }

        /// <summary>
        /// Adds a component to the the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public T AddComponent<T>(T component) where T : Component.Component
        {

            for (int i = 0; i < _components.Count; i++)
            {
                if (component.GetType() == _components[i].GetType())
                {
                    _components.Remove(_components[i]);
                }
            }

            _components.Add(component);
            return component;
        }

        public virtual void Update(GameTime gameTime)
        {
            
            var currentIndex = SceneManagement.Instance.CurrentScene.CellSpacePartition.PositionToIndex(
                GetComponent<TransformComponent>().Position);

            if (Index != currentIndex)
            {
                SceneManagement.Instance.CurrentScene.CellSpacePartition.ChangeEntityCell(Index, currentIndex, this);
                
                Index = currentIndex;
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {

            }
        }

        public override string ToString()
        {
            return Name ?? GetType().Name;
        }
    }
}
