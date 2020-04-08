using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Loki2D.Core.Utilities;
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
        private  World _world;

        public CellSpacePartition CellSpacePartition { get; set; }
        public Camera Camera { get; set; }

        public Point Size { get; set; } = new Point(5000,5000);

        // managers
        private RenderManager _renderManager;

        public Scene()
        {

        }
        
        public Scene(string name): this()
        {
            Name = name; 
        }

        public Scene(string name, Point size)
        {
            Name = name;
            Size = size;
        }
        
        public virtual void Initialize(GraphicsDevice graphicsDevice)
        {
            Camera = new Camera(graphicsDevice.Viewport);

            _world = new World(new AABB(Vector2.Zero, new Vector2(Size.X * CellSpacePartition.CellLength, Size.Y * CellSpacePartition.CellLength)));
            _world.Gravity = Vector2.Zero;

            _renderManager = new RenderManager();
            CellSpacePartition = new CellSpacePartition(Size.X, Size.Y);
            
            
            Console.WriteLine($"Initialized Scene: {Name}");
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
            Camera.Update();
            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _world.Step(totalSeconds);

            for (int y = 0; y < CellSpacePartition.Width; y++)
            {
                for (int x = 0; x < CellSpacePartition.Height; x++)
                {
                    CellSpacePartition.UpdateCell(x, y, gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _renderManager.Draw(spriteBatch);
        }
    }
}
