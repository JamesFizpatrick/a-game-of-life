using UnityEngine;
using UnityEngine.UI;


namespace GameOfLife.Cells
{
    public class Cell : MonoBehaviour
    {
        #region Fields

        [Header("Objects")]
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        [Header("Colors")] 
        [SerializeField] private Color deadColor;
        [SerializeField] private Color aliveColor;

        private CellState _nextCellState = CellState.None;
        
        #endregion


        
        #region Properties

        public RectTransform RectTransform => gameObject.GetComponent<RectTransform>();


        public CellState CurrentCellState { get; private set; }
        
        #endregion

        

        #region Unity lifecycle

        private void OnEnable() => button.onClick.AddListener(Button_OnClick);


        private void OnDisable() => button.onClick.RemoveListener(Button_OnClick);

        #endregion


        
        #region Public methods
        
        public void ForceSetCellState(CellState state)
        {
            CurrentCellState = state;
            SetCellColor(state);
        }


        public void PrepareCellState(CellState state)
        {
            _nextCellState = state;
        }


        public void ApplyPreparedCellState()
        {
            if (_nextCellState == CellState.None)
            {
                return;
            }

            CurrentCellState = _nextCellState;
            SetCellColor(CurrentCellState);
            _nextCellState = CellState.None;
        }

        
        public void SetButtonActivity(bool isActive) => button.interactable = isActive;


        public void SetBorder(float borderSize)
        {
            Vector2 offset = new Vector2(borderSize, borderSize);
            
            image.rectTransform.offsetMin = offset;
            image.rectTransform.offsetMax = -offset;
        }
        
        #endregion

        

        #region Private methods

        private void SetCellColor(CellState state)
        {
            switch (state)
            {
                case CellState.Alive:
                    image.color = aliveColor;
                    break;
                case CellState.Dead:
                    image.color = deadColor;
                    break;
            }
        }

        #endregion

        

        #region Event handlers

        private void Button_OnClick()
        {
            switch (CurrentCellState)
            {
                case CellState.Alive:
                    ForceSetCellState(CellState.Dead);
                    break;
                case CellState.Dead:
                    ForceSetCellState(CellState.Alive);
                    break;
            }
        }

        #endregion
    }
}
