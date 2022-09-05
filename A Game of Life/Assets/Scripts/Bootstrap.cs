using UnityEngine;


namespace GameOfLife.Game
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            Grid grid = new Grid();
            grid.CreateSquareGrid(10);
        }
    } 
}
