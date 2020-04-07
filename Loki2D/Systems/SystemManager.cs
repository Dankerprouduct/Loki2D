using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Systems
{
    public abstract class SystemManager<T> where T: class
    {
        public static T Instance { get; set; }

        protected SystemManager()
        {
            Instance = this as T;
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }


    }
}
