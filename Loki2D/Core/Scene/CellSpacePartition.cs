using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;

namespace Loki2D.Core.Scene
{
    public class Cell
    {
        public  int X { get; set; }
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


    }
}
