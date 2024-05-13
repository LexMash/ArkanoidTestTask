using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LevelEditor
{
    public class ClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector3> CellClicked;

        [SerializeField] private PositionToCellConverter _converter;

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector3 cellCenter = _converter.GetCellCoordinates(eventData.position);

            CellClicked?.Invoke(cellCenter);
        }
    }
}
