using GameOfLife.Cells;
using UnityEngine;


namespace GameOfLife.Grids
{
    public struct GridCellData
    {
        public Vector2Int Coordinates;
        public Cell CellObject;


        public GridCellData(Vector2Int coordinates, Cell cellObject)
        {
            Coordinates = coordinates;
            CellObject = cellObject;
        }
    }
}
