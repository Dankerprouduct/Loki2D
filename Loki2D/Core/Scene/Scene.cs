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
        private readonly World _world;

        public CellSpacePartition CellSpacePartition { get; set; }
        public Camera Camera { get; set; }

        public Point Size { get; set; } = new Point(5000,5000);

        public Scene()
        {

            _world = new World(new AABB(Vector2.Zero, Size.ToVector2()));
            _world.Gravity = Vector2.Zero;
            
            CellSpacePartition = new CellSpacePartition(Size.X, Size.Y);
        }
        
        public Scene(string name): this()
        {
            Name = name; 
        }

        public virtual void Initialize(GraphicsDevice graphicsDevice)
        {
            Camera = new Camera(graphicsDevice.Viewport);
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
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null,null, null, null, Camera.Transform);

            var topLeftX = (int)Camera.TopLeft.X;
            var topRightX = (int)Camera.TopRight.X;
            var topLeftY = (int) Camera.TopLeft.Y;
            var bottomLeftY = (int) Camera.BottomLeft.Y;

            for (int x = topLeftX; x < (int)(topRightX - topLeftX); x++)
            {
                for (int y = topLeftY; y < (int) (bottomLeftY - topLeftY); y++)
                {
                    CellSpacePartition.DrawCell(x,y, spriteBatch);
                }
            }

            spriteBatch.End();
        }
    }
}
