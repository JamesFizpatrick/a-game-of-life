using System.Collections;
using GameOfLife.Data;
using GameOfLife.Handlers;
using GameOfLife.UI;
using UnityEngine;
using Grid = GameOfLife.Grids.Grid;


namespace GameOfLife.Game
{
    public class InGameState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private GameScreen _screen;
        private Grid _grid;
        private Coroutine _loopCoroutine;


        public InGameState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        
        public void Enter()
        {
            _screen = (GameScreen)GameObject.Instantiate(DataContainer.GameObjects.GetScreen<GameScreen>(), SceneDefs.MainCanvas.transform);
            
            _screen.OnMenuClick += Screen_OnMenuClick;
            _screen.OnPauseClick += Screen_OnPauseClick;
            
            _grid = new Grid();
            _grid.CreateSquareGrid(30);

            _loopCoroutine = CoroutinesHandler.Instance.StartCoroutine(GameLoop(0.1f));
        }

        
        public void Exit()
        {
            if (_screen != null)
            {
                _screen.OnMenuClick -= Screen_OnMenuClick;
                _screen.OnPauseClick -= Screen_OnPauseClick;
            }
            
            CoroutinesHandler.Instance.StopCoroutine(_loopCoroutine);
        }
        
        
        private IEnumerator GameLoop(float interCyclesDelay)
        {
            while (true)
            {
                yield return new WaitForSeconds(interCyclesDelay);
                _grid.ProcessNextCycle();
            }
        }
        
        
        private void Screen_OnPauseClick()
        {
            if (_loopCoroutine != null)
            {
                CoroutinesHandler.Instance.StopCoroutine(_loopCoroutine);
                _loopCoroutine = null;
            }
            else
            {
                _loopCoroutine = CoroutinesHandler.Instance.StartCoroutine(GameLoop(0.1f));
            }
        }

        
        private void Screen_OnMenuClick() => _gameStateMachine.EnterState<MenuState>();
    }
}
