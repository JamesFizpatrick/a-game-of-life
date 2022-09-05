using System.Collections;
using UnityEngine;
using Grid = GameOfLife.Grids.Grid;


namespace GameOfLife.Game
{
    public class Bootstrap : MonoBehaviour
    {
        private Grid _grid;
        
        
        private void Start()
        {
            _grid = new Grid();
            _grid.CreateSquareGrid(15);

            StartCoroutine(GameLoop(0.5f));
        }

        
        private IEnumerator GameLoop(float interCyclesDelay)
        {
            while (true)
            {
                yield return new WaitForSeconds(interCyclesDelay);
                _grid.ProcessNextCycle();
            }
        }
    } 
}
