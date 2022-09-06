using GameOfLife.Cells;
using UnityEngine;


namespace GameOfLife.Data
{
    [CreateAssetMenu(fileName = "CustomAssets/GameObjects")]
    public class GameObjects : ScriptableObject
    {
        [SerializeField] private Cell gridCell;
        [SerializeField] private GameObject gridRoot;

        public Cell GridCell => gridCell;
        public GameObject GridRoot => gridRoot;
    }
}
