using GameOfLife.Cells;
using UnityEngine;


namespace GameOfLife.Data
{
    [CreateAssetMenu(fileName = "CustomAssets/GameObjects")]
    public class GameObjects : ScriptableObject
    {
        [SerializeField] private Cell gridCell;

        public Cell GridCell => gridCell;
    }
}
