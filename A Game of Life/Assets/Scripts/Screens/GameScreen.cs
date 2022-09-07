using System;
using UnityEngine;
using UnityEngine.UI;


namespace GameOfLife.UI
{
    public class GameScreen : BaseScreen
    {
        public Action OnPauseClick;
        public Action OnMenuClick;
    
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button menuButton;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(PauseButton_OnClick);
            menuButton.onClick.AddListener(MenuButton_OnClick);
        }
    
    
        private void OnDisable()
        {
            pauseButton.onClick.AddListener(PauseButton_OnClick);
            menuButton.onClick.AddListener(MenuButton_OnClick);
        }

    
        private void MenuButton_OnClick() => OnMenuClick?.Invoke();

    
        private void PauseButton_OnClick() => OnPauseClick?.Invoke();
    }
}
