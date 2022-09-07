using UnityEngine;


namespace GameOfLife.Game
{
    public class Bootstrap : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        
        private void Start()
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.EnterState<BootState>();
        }
    } 
}
