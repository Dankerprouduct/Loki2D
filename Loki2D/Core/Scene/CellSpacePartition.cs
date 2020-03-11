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

namespace Loki2D.Core.Scene
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }


        private  List< Entity> _entities = new List<Entity>();

        public bool AddEntity(Entity entity)
        {
            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
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
            foreach (var entity in _entities)
            {
                if (entity.HasComponent<RenderComponent>())
                {
                    entity.GetComponent<RenderComponent>().Draw(spriteBatch);
                }
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
        public const float CellWidth = 1024;

        private Cell[] _cells;

        public CellSpacePartition(int x, int y)
        {
            Width = x;
            Height = y; 

            _cells = new Cell[x * y];
            InitCells(_cells);
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

            var x = (int)(transform.X /= CellWidth);
            var y = (int)(transform.Y /= CellWidth);

            var index = x + Width * y;

            return _cells[index].AddEntity(entity);
        }

        public bool RemoveEntity(Entity entity)
        {
            var transform = entity.GetComponent<TransformComponent>().Position;

            var x = (int)(transform.X /= CellWidth);
            var y = (int)(transform.Y /= CellWidth);

            var index = x + Width * y;

            return _cells[index].RemoveEntity(entity);
        }


        public void UpdateCell(int x, int y, GameTime gameTime)
        {
            var _x = x /= (int)CellWidth;
            var _y = y /= (int)CellWidth;

            var index = _x + Width * _y;
            _cells[index].Update(gameTime);
        }

        public void UpdateCell(int index, GameTime gameTime)
        {
            _cells[index].Update(gameTime);
        }


        public void DrawCell(int x, int y, SpriteBatch spriteBatch)
        {
            var _x = x /= (int)CellWidth;
            var _y = y /= (int)CellWidth;

            var index = _x + Width * _y;
            _cells[index].Draw(spriteBatch);
        }

        public void DrawCell(int index, SpriteBatch spriteBatch)
        {
            _cells[index].Draw(spriteBatch);
        }

    }
}
