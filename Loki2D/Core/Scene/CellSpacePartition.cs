using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
using Loki2D.Core.Utilities;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Loki2D.Core.Scene
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Entity[] Entities => _entities?.ToArray();
        private List<Entity> _entities { get; set; }

        public void Initialize()
        {
            _entities = new List<Entity>();
        }

        public bool AddEntity(Entity entity)
        {
            if (_entities == null)
            {
                Initialize();
                Console.WriteLine($"Initialized: {X} {Y}");
            }
            

            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);

                _entities = _entities.OrderBy(e => e.GetComponent<RenderComponent>().RenderLayer).ToList();
                return true;
            }

            return false;
        }

        public Entity GetEntity(Vector2 position)
        {

            if (_entities == null) return null;

            Entity highestEntity = null;

            foreach (var entity in _entities)
            {
                var texture = entity.GetComponent<RenderComponent>().GetTexture();
                var vector2 = entity.GetComponent<TransformComponent>().Position;

                var width = (int)texture.Width;
                var height = (int)texture.Height;

                var vX = (int)(vector2.X - (width / 2));
                var vY = (int)(vector2.Y - (height / 2));

                var rect = new Rectangle(vX, vY, width, height);

                if (rect.Contains(position.ToPoint()))
                {
                    if (highestEntity == null)
                    {
                        highestEntity = entity;
                    }
                    else
                    {
                        if (entity.GetComponent<RenderComponent>().RenderLayer >
                            highestEntity.GetComponent<RenderComponent>().RenderLayer)
                        {
                            highestEntity = entity;
                        }
                    }

                }
            }

            return highestEntity;
        }

        public Entity GetEntity(Entity entity)
        {
            foreach (var e in _entities)
            {
                if (e == entity)
                    return e;
            }

            return null;
        }

        public bool RemoveEntity(Entity entity)
        {
            if (_entities == null)
            {
                return false;
            }

            if (_entities.Contains(entity))
            {
                _entities.Remove(entity);
                return true;
            }

            return false; 
        }

        public void Update(GameTime gameTime)
        {
            if(_entities == null)
                return;
            
            foreach (var entity in _entities)
            {
                if (entity.CanUpdate)
                {
                    entity.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_entities == null)
                return;
            foreach (var entity in _entities)
            {
                
            }
        }

        public int GetCellCount()
        {
            return _entities.Count; 
        }

    }

    public class CellSpacePartition
    {
        struct EntityChange
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Entity Entity { get; set; }
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public const float CellLength = 2048;
            
        public Cell[] Cells;
        private List<EntityChange> EntityChangeQueue = new List<EntityChange>();


        public CellSpacePartition(int x, int y)
        {
            Width = x;
            Height = y; 

            Cells = new Cell[x * y];
            InitCells(Cells);
        }

        public void InitCells(Cell[] cells)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell();
            }
        }

        public void ResetCells()
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Cell();
            }
        }

        public bool AddEntity(Entity entity)
        {
            var transform = entity.GetComponent<TransformComponent>().Position;

            var x = (int)(transform.X / CellLength);
            var y = (int)(transform.Y / CellLength);

            var index = x + Width * y;

            Cells[index].X = x;
            Cells[index].Y = y;  
            //Console.WriteLine($"Adding entity to cell {x} {y} {index}");
            return Cells[index].AddEntity(entity);
        }

        public Entity GetEntity(Vector2 position)
        {

            var x = (int)(position.X / CellLength);
            var y = (int)(position.Y / CellLength);

            var index = x + Width * y;

            if (index >= 0 && index <= CellLength)
            {
                return Cells[index].GetEntity(position);
            }

            return null;
        }
        
        public void ChangeEntityCell(int index, int newIndex, Entity entity)
        {

            EntityChangeQueue.Add(new EntityChange()
            {
                X = index,
                Y = newIndex,
                Entity = entity
            });
        }

        public void ExecuteChangeQueue()
        {
            for (int i = 0; i < EntityChangeQueue.Count; i++)
            {
                var item = EntityChangeQueue[i];

                var e = Cells[item.X].RemoveEntity(item.Entity);
                if (e)
                {
                    Cells[item.Y].AddEntity(item.Entity);
                }
                EntityChangeQueue.RemoveAt(i);
            }
        }

        public bool RemoveEntity(Entity entity)
        {
            var transform = entity.GetComponent<TransformComponent>().Position;

            var x = (int)(transform.X / CellLength);
            var y = (int)(transform.Y / CellLength);

            var index = x + Width * y;

            return Cells[index].RemoveEntity(entity);
        }
        
        public int PositionToIndex(Vector2 position)
        {
            var x = (int)(position.X / CellLength);
            var y = (int)(position.Y / CellLength);

            var index = x + Width * y;
            return index; 
        }

        public Cell RetrieveCell(int x, int y)
        {
            var index = x + Width * y;
            return Cells[index];
        }

        public void UpdateCell(int x, int y, GameTime gameTime)
        {
            var index = x + Width * y;
            Cells[index].Update(gameTime);
            ExecuteChangeQueue();
        }

        public void UpdateCell(int index, GameTime gameTime)
        {
            Cells[index].Update(gameTime);
            ExecuteChangeQueue();
        }
        
        public void DrawCell(int x, int y, SpriteBatch spriteBatch)
        {
            var index = x + Width * y;
            Cells[index].Draw(spriteBatch);

        }

        public void DrawCell(int index, SpriteBatch spriteBatch)
        {
            
            Cells[index].Draw(spriteBatch);
        }

    }
}
