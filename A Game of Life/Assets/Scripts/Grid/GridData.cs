using UnityEngine;


namespace GameOfLife.Grids
{
    public struct GridData
    {
        public GameObject GridRootObject;
        public Vector2Int GridDimensions;
        public int CellDimension;

        
        public GridData(GameObject gridRootObject, Vector2Int gridDimensions, int cellDimension)
        {
            GridRootObject = gridRootObject;
            GridDimensions = gridDimensions;
            CellDimension = cellDimension;
        }
    }
}
