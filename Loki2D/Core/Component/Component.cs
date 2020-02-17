using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Component
{
    public class Component
    {
        public string Name { get; set; }
        public bool CanDraw { get; set; }

        public Entity Owner { get; set; }

        public Component(Entity entity)
        {
            Owner = entity; 
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        
    }
}
