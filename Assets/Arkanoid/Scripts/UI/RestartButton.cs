using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Arkanoid.UI
{
    public class RestartButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action RestartPerformed;

        public void OnPointerClick(PointerEventData eventData)
        {
            RestartPerformed?.Invoke();
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
