using System.Collections.Generic;
using System.Linq;
using GameOfLife.Cells;
using GameOfLife.Data;
using GameOfLife.Handlers;
using UnityEngine;
using Random = System.Random;


namespace GameOfLife.Grids
{
    public class Grid
    {
        #region Fields

        private const string GridRootObjectName = "===GRID===";
        private const int CellBorder = 10;
        
        private readonly Cell _cellPrefab;
        private readonly Vector2 _cellDimensions;
        private readonly Random _random;

        private List<GridCellData> _cells = new List<GridCellData>();
        private GameObject _gridRootObject;

        private Vector2Int currentGridDimensions = Vector2Int.zero;
        
        #endregion



        #region Class lifecycle

        public Grid()
        {
            _cellPrefab = DataContainer.GameObjects.GridCell;
            _cellDimensions = _cellPrefab.RectTransform.sizeDelta;
            _random = new Random();
        }

        #endregion


        #region Public methods
        
        public void CreateSquareGrid(int dimension)
        {
            if (_gridRootObject == null)
            {
                CreateGridRootObject();
            }
            
            if (_cells.Count != 0)
            {
                ClearGrid();
            }

            currentGridDimensions = new Vector2Int(dimension, dimension);
            
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    Cell cell = GameObject.Instantiate(_cellPrefab, _gridRootObject.transform);
                    cell.RectTransform.localPosition = new Vector2(i * (_cellDimensions.x + CellBorder), j * (_cellDimensions.y + CellBorder));
                    
                    cell.ForceSetCellState(GetRandomCellState());
                    cell.SetButtonActivity(false);

                    GridCellData data = new GridCellData(new Vector2Int(i, j), cell);
                    _cells.Add(data);
                }
            }
        }


        public void CreateGridRootObject()
        {
            _gridRootObject = new GameObject(GridRootObjectName);
                
            _gridRootObject.transform.parent = SceneDefs.MainCanvas.transform;
            _gridRootObject.transform.localPosition = Vector3.zero;
            _gridRootObject.transform.localRotation = Quaternion.identity;
        }
        
        
        public void ClearGrid()
        {
            foreach (GridCellData cell in _cells)
            {
                GameObject.Destroy(cell.CellObject.gameObject);
            }
            
            _cells.Clear();
            currentGridDimensions = Vector2Int.zero;
        }


        public void ProcessNextCycle()
        {
            // Prepare next cell states
            foreach (GridCellData cell in _cells)
            {
                TEST();
                List<Cell> neighbours = GetNeighbouringCells(cell.Coordinates);
                CellState newState = CellState.Dead;
                
                int aliveNeighboursCount = neighbours.Count(cell => cell.CurrentCellState == CellState.Alive);
                
                if (cell.CellObject.CurrentCellState == CellState.Dead)
                {
                    if (aliveNeighboursCount == 3)
                    {
                        newState = CellState.Alive;
                    }
                }
                else if (cell.CellObject.CurrentCellState == CellState.Alive)
                {
                    if (aliveNeighboursCount == 2 || aliveNeighboursCount == 3)
                    {
                        newState = CellState.Alive;
                    }
                }
                
                cell.CellObject.PrepareCellState(newState);
            }
            
            // apply next cell states
            foreach (GridCellData cell in _cells)
            {
                cell.CellObject.ApplyPreparedCellState();
            }
        }
        
        #endregion



        #region Private methods

        private List<Cell> GetNeighbouringCells(Vector2Int coordinates)
        {
            List<Cell> result = new List<Cell>();
            List<Vector2Int> neighbourCoordinates = GetNeighbourCoordinates(coordinates);
            
            foreach (Vector2Int neighbour in neighbourCoordinates)
            {
                GridCellData cell = _cells.FirstOrDefault(cell =>
                    cell.Coordinates.x == neighbour.x && cell.Coordinates.y == neighbour.y);
                
                result.Add(cell.CellObject);
            }
            
            return result;
        }


        private void TEST()
        {
            List<Vector2Int> neighbourCoordinates = GetNeighbourCoordinates(new Vector2Int(0, 0));
            
            foreach (Vector2Int neighbour in neighbourCoordinates)
            {
                // Debug.LogError($"{neighbour.x} : {neighbour.y}");
            }
            
            // Debug.LogError("////////");
        }
        

        private List<Vector2Int> GetNeighbourCoordinates(Vector2Int coordinates)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            
            int minX = 0;
            int minY = 0;
            int maxX = currentGridDimensions.x - 1;
            int maxY = currentGridDimensions.y - 1;
            
            for (int x = coordinates.x - 1; x <= coordinates.x + 1; x++)
            {
                for (int y = coordinates.y - 1; y <= coordinates.x + 1; y++)
                {
                    if (x == coordinates.x && y == coordinates.y)
                    {
                        continue;
                    }
                    
                    int xCoord = x;
                    int yCoord = y;
                    
                    if (x > maxX)
                    {
                        xCoord = x - maxX - 1;
                    }
                    else if (x < minX)
                    {
                        xCoord = currentGridDimensions.x + x;
                    }
                    
                    if (y > maxY)
                    {
                        yCoord = y - maxY - 1;
                    }
                    else if (y < minY)
                    {
                        yCoord = currentGridDimensions.y + y;
                    }
                    
                    result.Add(new Vector2Int(xCoord, yCoord));
                }
            }

            return result;
        }
        
        
        private CellState GetRandomCellState()
        {
            int decision = _random.Next(0, 2);
            return decision == 0 ? CellState.Alive : CellState.Dead;
        }

        #endregion
    }
}
