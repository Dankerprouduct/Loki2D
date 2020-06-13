using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Attributes;
using Loki2D.Core.GameObject;
using Microsoft.Xna.Framework;

namespace Loki2D.Core.Component
{
    [EditorInspectable("Transform Component")]
    public class TransformComponent:Component
    {
        /// <summary>
        /// The position of the entity
        /// </summary>
        [EditorInspectable]
        public Vector2 Position { get; set; }

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
            Position = position;

            Initialize();
        }
    }
}
