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
            _grid.CreateSquareGrid(20);

            StartCoroutine(GameLoop(0.1f));
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     _grid.ProcessNextCycle();
            // }
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
