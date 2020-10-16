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
        /// <summary>
        /// The ID associated with this entity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name associated with this entity
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines whether or not this entity can be updated
        /// </summary>
        public bool CanUpdate { get; set; }

        /// <summary>
        /// Determines whether or not this entity respects spatial partition updates
        /// False - update when partition cell is loaded
        /// True - update always
        /// </summary>
        public bool AlwaysUpdate { get; set; }
        
        /// <summary>
        /// The type associated with this entity
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// The cell partition index
        /// </summary>
        public int Index;

        /// <summary>
        /// The Components owned by this entity
        /// </summary>
        public Component.Component[] Components => _components?.ToArray();
        internal List<Component.Component> _components = new List<Component.Component>();

        /// <summary>
        /// The Parent of this Entity; 
        /// </summary>
        public Entity Parent { get; set; }
        
        /// <summary>
        /// Base class for all entities
        /// </summary>
        public Entity()
        {
            CanUpdate = true;
            EntityType = GetType().FullName;
        }

        /// <summary>
        /// Base class for all entities
        /// </summary>
        /// <param name="id">The entity Id</param>
        /// <param name="name">The entity name</param>
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
        /// Called every time this entity is loaded
        /// </summary>
        public virtual void Init()
        {

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

        /// <summary>
        /// Sets this entity's parent. 
        /// </summary>
        /// <param name="entity"></param>
        public void SetParent(Entity entity)
        {
            Parent = entity; 
        }

        /// <summary>
        /// Clears this entity's parent
        /// </summary>
        public void ClearParent()
        {
            Parent = null;
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="gameTime"></param>
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

        /// <summary>
        /// Removes this entity from the scene. 
        /// </summary>
        public void Destroy()
        {
            SceneManagement.Instance.CurrentScene.RemoveEntity(this);
        }

        /// <summary>
        /// Called every frame. NOT CURRENTLY IN USE
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {

            }
        }

        /// <summary>
        /// Returns the name of this entity
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name ?? GetType().Name;
        }
    }
}
