using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Attributes;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Loki2D.Core.Component
{
    [EditorInspectable("Transform Component")]
    public class TransformComponent:Component
    {
        [JsonIgnore]
        public Entity Owner { get; set; }

        /// <summary>
        /// The position of the entity
        /// </summary>
        [EditorInspectable]
        public Vector2 Position
        {
            get
            {
                if (Owner.Parent == null)
                    return _position;
                return Owner.Parent.GetComponent<TransformComponent>().Position + _position;
            }
            set => _position = value;
        }

        private Vector2 _position { get; set; }

        public TransformComponent()
        {

        }

        /// <summary>
        /// Used for designating an entity's transform
        /// </summary>
        /// <param name="entity">parent entity</param>
        /// <param name="position">position</param>
        public TransformComponent(Entity entity, Vector2 position)
        {
            Owner = entity;
            Position = position;

            Initialize();

            var currentIndex = SceneManagement.Instance.CurrentScene.CellSpacePartition.PositionToIndex(Position);
            Owner.Index = currentIndex; 
        }
    }
}
