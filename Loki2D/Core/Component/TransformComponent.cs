using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Microsoft.Xna.Framework;

namespace Loki2D.Core.Component
{
    public class TransformComponent:Component
    {
        public Vector2 Position { get; set; }

        public TransformComponent(Entity entity) : base(entity)
        {

        }

        public TransformComponent(Entity entity, Vector2 position) : base(entity)
        {
            Position = position; 
        }
    }
}
