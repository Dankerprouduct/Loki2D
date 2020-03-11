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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                TextureManager.Instance.GetTexture(TextureName),
                Owner.GetComponent<TransformComponent>().Position,
                null, Color.White,
                0f,
                TextureManager.Instance.GetTexture(TextureName).Bounds.Center.ToVector2(),
                Scale, SpriteEffects.None, RenderLayer);
        }
    }
}
