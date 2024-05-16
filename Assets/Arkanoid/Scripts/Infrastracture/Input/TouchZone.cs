using Arkanoid.Input;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TouchZone : MonoBehaviour, IInput, IPointerClickHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _rectTransform;
    
    public event Action ActionPerformed;
    public event Action<Vector3> MovePerformed;

    private Camera _camera;

//#if UNITY_EDITOR
//    private void Awake()
//    {
//        _camera = Camera.main;
//    }
//#endif

    [Inject]
    private void Construct(Camera camera)
    {
        _camera = camera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ActionPerformed?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Move(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Move(eventData);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Move(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position, _camera, out Vector3 worldPoint);
        MovePerformed?.Invoke(worldPoint);
    }
}
