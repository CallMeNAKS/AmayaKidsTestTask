using UnityEngine;

namespace CodeBase.Grid
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomGridLayout : MonoBehaviour
    {
        [SerializeField] private float _cellWidth = 220f;
        [SerializeField] private float _cellHeight = 220f;
        [SerializeField] private Vector2 _spacing = Vector2.zero;

        private int _rows;
        private int _columns;

        public void SetNewGridSize(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            ArrangeElements();
        }

        private void ArrangeElements()
        {
            int index = 0;
            foreach (Transform child in transform)
            {
                if (child == null) continue;

                child.transform.localScale = Vector3.one;

                if (index >= _rows * _columns) break;

                int row = index / _columns;
                int column = index % _columns;

                RectTransform childRect = child.GetComponent<RectTransform>();

                float xPos = (column - (_columns - 1) / 2f) * (_cellWidth + _spacing.x);
                float yPos = -(row - (_rows - 1) / 2f) * (_cellHeight + _spacing.y);
                
                childRect.sizeDelta = new Vector2(_cellWidth, _cellHeight);

                childRect.anchorMin = new Vector2(0.5f, 0.5f);
                childRect.anchorMax = new Vector2(0.5f, 0.5f);
                childRect.pivot = new Vector2(0.5f, 0.5f);
                childRect.anchoredPosition = new Vector2(xPos, yPos);

                index++;
            }
        }
    }
}