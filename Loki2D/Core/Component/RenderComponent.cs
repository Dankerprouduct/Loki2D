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
using Newtonsoft.Json;

namespace Loki2D.Core.Component
{
    [EditorInspectable("Render Component")]
    public class RenderComponent: Component, IComparable<RenderComponent>
    {
        /// <summary>
        /// The render layer of the texture
        /// </summary>
        [EditorInspectable]
        public int RenderLayer { get; set; } = 0;

        /// <summary>
        /// The name of the texture
        /// </summary>
        [EditorInspectable("Texture Name")]
        public string TextureName { get; set; }

        /// <summary>
        /// The scale of the texture
        /// </summary>
        [EditorInspectable]
        public float Scale { get; set; } = 1;

        /// <summary>
        /// The rotation of the texture
        /// </summary>
        [EditorInspectable]
        public float Rotation { get; set; } = 0;

        [EditorInspectable]
        public Vector2 Origin { get; set; }

        [EditorInspectable]
        public bool UsesCustomOrigin { get; set; }

        [EditorInspectable]
        public bool OverrideRender { get; set; }

        /// <summary>
        /// The material being used by the component
        /// </summary>
        [JsonIgnore]
        public Material Material { get; set; }

        public event EventHandler<DrawEventArgs> OnDraw; 

        public RenderComponent()
        {

        }

        /// <summary>
        /// Render component
        /// </summary>
        /// <param name="textureName">The name of the texture to be used</param>
        public RenderComponent(string textureName)
        {
            TextureName = textureName;
            Initialize();
        }

        ~RenderComponent()
        {
            RenderManager.Instance.UnRegisterComponent(this);
        }

        /// <summary>
        /// Sets the material to be used for deferred rendering
        /// </summary>
        /// <param name="material"></param>
        public void SetMaterial(Material material)
        {
            Material = material;
        }

        public override void Initialize()
        {
            base.Initialize();
            RenderManager.Instance.RegisterComponent(this);
        }

        /// <summary>
        /// Returns the texture used in rendering
        /// </summary>
        /// <returns></returns>
        public Texture2D GetTexture()
        {
            return TextureManager.Instance.GetTexture(TextureName);
        }

        public int CompareTo(RenderComponent other)
        {
            return other.RenderLayer > RenderLayer ? 1 : 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            OnDraw?.Invoke(this, new DrawEventArgs()
            {
                SpriteBatch = spriteBatch
            });
        }

    }

    public class DrawEventArgs : EventArgs
    {
        public SpriteBatch SpriteBatch { get; set; }
    }
}
