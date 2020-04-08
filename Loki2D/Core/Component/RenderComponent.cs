using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Component
{
    public class RenderComponent: Component, IComparable<RenderComponent>
    {
        public int RenderLayer { get; set; } = 0;
        public string TextureName { get; set; }
        public float Scale { get; set; } = 1;
        public float Rotation { get; set; } = 0;

        public RenderComponent()
        {

        }

        public RenderComponent(string textureName)
        {
            TextureName = textureName;
            Initialize();
        }

        ~RenderComponent()
        {
            RenderManager.Instance.UnRegisterComponent(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            RenderManager.Instance.RegisterComponent(this);
        }

        public int CompareTo(RenderComponent other)
        {
            return other.RenderLayer > RenderLayer ? 1 : 0;
        }

    }
}
