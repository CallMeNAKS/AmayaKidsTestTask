using System;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Card
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Card> OnClick;

        [field: SerializeField] public Image CardImage { get; private set; }
        public string CardName { get; private set; }

        public void Init(Sprite cardSprite, string cardName)
        {
            CardImage.sprite = cardSprite;
            CardName = cardName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}