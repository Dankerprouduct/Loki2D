using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Loki2D.Systems;
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

        public CellSpacePartition CellSpacePartition { get; set; }
        
        public Scene()
        {
            _world = new World(new AABB(Vector2.Zero, new Vector2(5000,5000)));
            _world.Gravity = Vector2.Zero;
            
            CellSpacePartition = new CellSpacePartition(5000,5000);
        }
        
        public Scene(string name): this()
        {
            Name = name; 
        }

        public bool AddEntity(Entity entity)
        {
            return CellSpacePartition.AddEntity(entity);
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
