using System.Collections.Generic;
using GameOfLife.Data;
using GameOfLife.Handlers;
using UnityEngine;


namespace GameOfLife
{
    public class Grid
    {
        private const string gridRootObjectName = "===GRID===";
        
        private List<Cell> cells = new List<Cell>();
        private Cell cellPrefab;
        private Vector2 cellDimensions;
        private GameObject gridRootObject;
        
        
        public Grid()
        {
            cellPrefab = DataContainer.GameObjects.GridCell;
            cellDimensions = cellPrefab.GetRectTransform().sizeDelta;
        }

        
        public void CreateSquareGrid(int dimension)
        {
            if (gridRootObject == null)
            {
                CreateGridRootObject();
            }
            
            if (cells.Count != 0)
            {
                ClearGrid();
            }
            
            for (float i = -dimension/2f; i < dimension/2f; i++)
            {
                for (float j = -dimension/2f; j < dimension/2f; j++)
                {
                    Cell cell = GameObject.Instantiate(cellPrefab, gridRootObject.transform);
                    cell.GetRectTransform().localPosition = new Vector2(i * cellDimensions.x, j * cellDimensions.y);
                    cells.Add(cell);
                }
            }
        }


        public void CreateGridRootObject()
        {
            gridRootObject = new GameObject(gridRootObjectName);
                
            gridRootObject.transform.parent = SceneDefs.MainCanvas.transform;
            gridRootObject.transform.localPosition = Vector3.zero;
            gridRootObject.transform.localRotation = Quaternion.identity;
        }
        
        
        public void ClearGrid()
        {
            foreach (Cell cell in cells)
            {
                GameObject.Destroy(cell.gameObject);
            }
            
            cells.Clear();
        }


        public List<Cell> GetNeighbouringCells(Vector2Int coordinates)
        {
            return null;
        }
    }
}
