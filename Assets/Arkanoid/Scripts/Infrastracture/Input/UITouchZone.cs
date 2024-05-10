using Arkanoid.Input;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITouchZone : MonoBehaviour, IInput, IPointerClickHandler, IDragHandler, IPointerDownHandler
{
    [field: SerializeField] public RectTransform RectTransform;

    public event Action ActionPerformed;
    public event Action<Vector2> MovePerformed;

    public void OnPointerClick(PointerEventData eventData)
    {
        ActionPerformed?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        MovePerformed?.Invoke(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MovePerformed?.Invoke(eventData.position);
    }
}
