using System.Collections.Generic;
using System;


namespace GameOfLife.Game
{
    public class GameStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootState)] = new BootState(this),
                [typeof(InGameState)] = new InGameState(this),
                [typeof(MenuState)] = new MenuState(this)
            };
        }
    
    
        public void EnterState<TStateType>() where TStateType : class, IState
        {
            IState newState = ChangeState<TStateType>();
            newState.Enter();
        }
    
    
        private TStateType GetState<TStateType>() where TStateType : class, IState =>
            _states[typeof(TStateType)] as TStateType;

    
        private TStateType ChangeState<TStateType>() where TStateType : class, IState
        {
            _currentState?.Exit();

            TStateType newState = GetState<TStateType>();
            _currentState = newState;
            
            return newState;
        }
    }
}
