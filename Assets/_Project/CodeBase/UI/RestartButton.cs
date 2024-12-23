using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI
{
    public class RestartButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}