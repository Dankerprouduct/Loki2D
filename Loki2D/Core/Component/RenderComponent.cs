using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;

namespace Loki2D.Core.Component
{
    public class RenderComponent: Component, IComparable<RenderComponent>
    {
        public int RenderLayer { get; set; } = 0;
        public string TextureName { get; set; }

        public RenderComponent(Entity entity) : base(entity)
        {
            
        }

        public RenderComponent(Entity entity, string textureName) : base(entity)
        {
            TextureName = textureName;
        }
        
        public int CompareTo(RenderComponent other)
        {
            return other.RenderLayer > RenderLayer ? 1 : 0;
        }
    }
}
