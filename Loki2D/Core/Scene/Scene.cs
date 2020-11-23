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
using SharpDX.MediaFoundation;
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

        /// <summary>
        /// Returns the current physics world
        /// </summary>
        /// <returns></returns>
        public World GetPhysicsWorld()
        {
            return _world;
        }
        
        /// <summary>
        /// Adds an entity to the scene
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddEntity(Entity entity)
        {
            try
            {
                return CellSpacePartition.AddEntity(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        /// <summary>
        /// Retrieves an entity based off a position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Entity GetEntity(Vector2 position)
        {
            return CellSpacePartition.GetEntity(position);
        }

        public List<Entity> GetEntitiesByTag(string tag)
        {
            List<Entity> allEntities = new List<Entity>();
            for (int i = 0; i < CellSpacePartition.Cells.Length; i++)
            {
                var cell = CellSpacePartition.Cells[i];
                if (cell != null)
                {
                    if (cell.Entities != null)
                    {
                        foreach (var entity in cell.Entities)
                        {
                            allEntities.Add(entity);
                        }
                    }
                }
            }

            return allEntities.Where(i => i.Tag == tag).ToList();
        }
        
        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(Entity entity)
        {
            CellSpacePartition.RemoveEntity(entity);
        }

        /// <summary>
        /// Adds a physics body to the scene
        /// </summary>
        /// <param name="body"></param>
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
