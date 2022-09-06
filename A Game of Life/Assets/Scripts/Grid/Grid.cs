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
        
        private readonly Cell _cellPrefab;
        private readonly Random _random;

        private List<GridCellData> _cells = new List<GridCellData>();

        private GameObject _gridRootObject;
        private GridData _currentGridData;
        
        #endregion



        #region Class lifecycle

        public Grid()
        {
            _cellPrefab = DataContainer.GameObjects.GridCell;
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

            _currentGridData = CreateGrid(dimension, dimension);
            _cells = CreateCells(_currentGridData);
        }
        
        
        public void CreateGridRootObject()
        {
            _gridRootObject = GameObject.Instantiate(DataContainer.GameObjects.GridRoot, SceneDefs.MainCanvas.transform);
                
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
            _currentGridData = new GridData(null, Vector2Int.zero, 0);
        }


        public void ProcessNextCycle()
        {
            // Prepare next cell states
            foreach (GridCellData cell in _cells)
            {
                List<Cell> neighbours = GetNeighbouringCells(cell.Coordinates);
                CellState newState = CellState.Dead;
                
                int aliveNeighboursCount = neighbours.Count(x => x.CurrentCellState == CellState.Alive);
                
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

        
        private List<Vector2Int> GetNeighbourCoordinates(Vector2Int coordinates)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            
            int minX = 0;
            int minY = 0;
            int maxX = _currentGridData.GridDimensions.x - 1;
            int maxY = _currentGridData.GridDimensions.y - 1;
            
            for (int x = coordinates.x - 1; x <= coordinates.x + 1; x++)
            {
                for (int y = coordinates.y - 1; y <= coordinates.y + 1; y++)
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
                        xCoord = _currentGridData.GridDimensions.x + x;
                    }
                    
                    if (y > maxY)
                    {
                        yCoord = y - maxY - 1;
                    }
                    else if (y < minY)
                    {
                        yCoord = _currentGridData.GridDimensions.y + y;
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


        private GridData CreateGrid(int width, int height)
        {
            Vector2Int gridDimensions = new Vector2Int(width, height);

            int cellDimension;
            if (Screen.width < Screen.height)
            {
                cellDimension = Screen.width / width;
            }
            else
            {
                cellDimension = Screen.height / height;
            }
            
            return new GridData(_gridRootObject, gridDimensions, cellDimension);
        }


        private List<GridCellData> CreateCells(GridData gridData)
        {
            List<GridCellData> result = new List<GridCellData>();

            float borderedCellSize = gridData.CellDimension;
            float halfCellSize = gridData.CellDimension / 2f;
            float startXCoordinate = -gridData.GridDimensions.x / 2f * borderedCellSize + halfCellSize;
            float startYCoordinate = -gridData.GridDimensions.y / 2f * borderedCellSize + halfCellSize;

            Vector2 cellPosition = new Vector2(startXCoordinate, startYCoordinate);

            for (int x = 0; x < gridData.GridDimensions.x; x++)
            {
                for (int y = 0; y < gridData.GridDimensions.y; y++)
                {
                    GridCellData cell = CreateCell(x, y, gridData.CellDimension, 2);
                    cell.CellObject.RectTransform.localPosition = cellPosition;
                    result.Add(cell);

                    cellPosition.y += gridData.CellDimension;
                }

                cellPosition.y = startYCoordinate;
                cellPosition.x += gridData.CellDimension;
            }

            return result;
        }
        
            
        private GridCellData CreateCell(int x, int y, int cellDimension, float border = 0f)
        {
            Cell cell = GameObject.Instantiate(_cellPrefab, _gridRootObject.transform);
            cell.RectTransform.sizeDelta = new Vector2(cellDimension, cellDimension);
            
            cell.ForceSetCellState(GetRandomCellState());
            cell.SetButtonActivity(true);
            cell.SetBorder(border);

            GridCellData data = new GridCellData(new Vector2Int(x, y), cell);
            return data;
        }
        
        #endregion
    }
}
