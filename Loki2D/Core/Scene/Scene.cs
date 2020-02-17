using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Dynamics;

namespace Loki2D.Core.Scene
{
    public class Scene
    {
        public string Name { get; set; }
        private readonly World _world;

        public Scene()
        {
            _world = new World(new AABB(Vector2.Zero, new Vector2(5000,5000)));
            _world.Gravity = Vector2.Zero;
        }
        
        public Scene(string name)
        {
            Name = name; 
        }

        public void AddBody(Body body)
        {
            _world.Add(body);
        }

        public void Update(GameTime gameTime)
        {
            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _world.Step(totalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
