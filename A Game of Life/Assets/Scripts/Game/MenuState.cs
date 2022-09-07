using GameOfLife.Data;
using GameOfLife.Handlers;
using GameOfLife.UI;
using UnityEngine;

namespace GameOfLife.Game
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private MenuScreen _screen;

        public MenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _screen = (MenuScreen)GameObject.Instantiate(DataContainer.GameObjects.GetScreen<MenuScreen>(), SceneDefs.MainCanvas.transform);
        }

        public void Exit()
        {
        }
    }
}