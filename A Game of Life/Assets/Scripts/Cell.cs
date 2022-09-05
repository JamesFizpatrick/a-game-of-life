using UnityEngine;
using UnityEngine.UI;


namespace GameOfLife
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public RectTransform GetRectTransform() => gameObject.GetComponent<RectTransform>();
    }
}
