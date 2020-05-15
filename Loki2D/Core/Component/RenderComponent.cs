using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Attributes;
using Loki2D.Core.GameObject;
using Loki2D.Core.Shaders;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Component
{
    [EditorInspectable("Render Component")]
    public class RenderComponent: Component, IComparable<RenderComponent>
    {
        [EditorInspectable]
        public int RenderLayer { get; set; } = 0;

        [EditorInspectable("Texture Name")]
        public string TextureName { get; set; }

        [EditorInspectable]
        public float Scale { get; set; } = 1;

        [EditorInspectable]
        public float Rotation { get; set; } = 0;

        public Material Material;

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

        public void SetMaterial(Material material)
        {
            Material = material;
        }

        public override void Initialize()
        {
            base.Initialize();
            RenderManager.Instance.RegisterComponent(this);
        }

        public Texture2D GetTexture()
        {
            return TextureManager.Instance.GetTexture(TextureName);
        }

        public int CompareTo(RenderComponent other)
        {
            return other.RenderLayer > RenderLayer ? 1 : 0;
        }

    }
}
