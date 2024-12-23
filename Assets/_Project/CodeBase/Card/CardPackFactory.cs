using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Card
{
    public class CardPackFactory
    {
        private readonly Card _cardPrefab;

        public CardPackFactory(Card cardPrefab)
        {
            _cardPrefab = cardPrefab;
        }

        public List<Card> CreateCardPack(CardPack cardPack, int count)
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                var newCard = Object.Instantiate(_cardPrefab);
                cards.Add(newCard);
            }

            return cards;
        }
    }
}