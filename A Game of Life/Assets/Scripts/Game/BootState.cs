using GameOfLife.Data;
using GameOfLife.Handlers;
using UnityEngine;


namespace GameOfLife.Game
{
    public class BootState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        
        public BootState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        
        public void Enter()
        {
            GameObject.Instantiate(DataContainer.GameObjects.Background, SceneDefs.MainCanvas.transform);
            _gameStateMachine.EnterState<InGameState>();
        }

        
        public void Exit() { }
    }
}