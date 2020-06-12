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
using Newtonsoft.Json;
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
        public GUIManager GuiManager; 

        public Point Size { get; set; } = new Point(5000,5000);
        public bool DeferredDraw = false; 
        [JsonIgnore]
        public GraphicsDevice GraphicsDevice { get; set; }
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
            GraphicsDevice = graphicsDevice;

            Camera = new Camera(graphicsDevice.Viewport);

            _world = new World();
            

            _renderManager = new RenderManager();
            CellSpacePartition = new CellSpacePartition(Size.X, Size.Y);
            GuiManager = new GUIManager();
            
            Console.WriteLine($"Initialized Scene: {Name}");
        }

        public World GetPhysicsWorld()
        {
            return _world;
        }

        public bool AddEntity(Entity entity)
        {
            return CellSpacePartition.AddEntity(entity);
        }

        public Entity GetEntity(Vector2 position)
        {
            return CellSpacePartition.GetEntity(position);
        }




        public void RemoveEntity(Entity entity)
        {
            CellSpacePartition.RemoveEntity(entity);
        }

        public void AddBody(Body body)
        {
            _world.Add(body);
            Console.WriteLine("Added body");
        }

        public void Update(GameTime gameTime)
        {
            GuiManager.Update(gameTime);
            Camera.Update();
            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < 5; i++)
            {
                _world.Step(totalSeconds);
            }

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
            if (!DeferredDraw)
            {
                _renderManager.Draw(spriteBatch);
            }
            else
            {
                _renderManager.DeferredDraw(spriteBatch);

                _renderManager.DrawDebugRenderTargets(spriteBatch);
            }

            // gui
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            GuiManager.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
