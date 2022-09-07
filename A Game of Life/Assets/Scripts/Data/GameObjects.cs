using System.Linq;
using GameOfLife.Cells;
using GameOfLife.UI;
using UnityEngine;


namespace GameOfLife.Data
{
    [CreateAssetMenu(fileName = "CustomAssets/GameObjects")]
    public class GameObjects : ScriptableObject
    {
        #region Fields

        [Header("Grid")]
        [SerializeField] private Cell gridCell;
        [SerializeField] private GameObject gridRoot;
        
        [Header("Misc")]
        [SerializeField] private GameObject background;
        
        [Header("Screens")]
        [SerializeField] private BaseScreen[] screenPrefabs;

        #endregion

        public Cell GridCell => gridCell;
        
        public GameObject GridRoot => gridRoot;
        
        public GameObject Background => background;
        
        
        public BaseScreen GetScreen<TScreenType>() =>
            screenPrefabs.FirstOrDefault(x => x.GetType() == typeof(TScreenType));
    }
}
