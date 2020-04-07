using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
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

                Console.WriteLine($"added entity to {X} {Y}");
                return true;
            }

            return false;
        }

        public bool RemoveEntity(Entity entity)
        {
            if (!_entities.Contains(entity))
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
        public int Width { get; set; }
        public int Height { get; set; }
        public const float CellLength = 1024;
            
        public Cell[] Cells;

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

        public bool AddEntity(Entity entity)
        {
            var transform = entity.GetComponent<TransformComponent>().Position;

            var x = (int)(transform.X /= CellLength);
            var y = (int)(transform.Y /= CellLength);

            var index = x + Width * y;

            Cells[index].X = x;
            Cells[index].Y = y;  
            Console.WriteLine($"Adding entity to cell {x} {y} {index}");
            return Cells[index].AddEntity(entity);
        }

        public bool RemoveEntity(Entity entity)
        {
            var transform = entity.GetComponent<TransformComponent>().Position;

            var x = (int)(transform.X /= CellLength);
            var y = (int)(transform.Y /= CellLength);

            var index = x + Width * y;

            return Cells[index].RemoveEntity(entity);
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
        }

        public void UpdateCell(int index, GameTime gameTime)
        {
            Cells[index].Update(gameTime);
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
