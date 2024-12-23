using System.Collections.Generic;
using CodeBase.Card;
using CodeBase.Grid;
using UnityEngine;

namespace CodeBase.Level
{
    public class CardCreator
    {
        private readonly CustomGridLayout _gridLayout;
        private readonly CardPackFactory _cardPackFactory;

        private List<Card.Card> _cards = new List<Card.Card>();
        private List<CardPack.Card> _cardData = new List<CardPack.Card>();

        private const float BounceDuration = 1f;
        private const int Bounciness = 1;

        public CardCreator(CustomGridLayout gridLayout,
            CardPackFactory cardPackFactory)
        {
            _gridLayout = gridLayout;
            _cardPackFactory = cardPackFactory;
        }

        public List<Card.Card> CreateCards(LevelStageConfig stageData)
        {
            CreateCard(stageData);
            SetCardData(stageData);
            ArrangeElements(stageData);

            return _cards;
        }

        private void ArrangeElements(LevelStageConfig stageData)
        {
            foreach (var card in _cards)
            {
                card.transform.SetParent(_gridLayout.transform);
            }

            _gridLayout.SetNewGridSize(stageData.Rows, stageData.Columns);
        }

        public void Reset()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                GameObject.DestroyImmediate(_cards[i].gameObject);
            }

            _cards.Clear();
        }

        private void SetCardData(LevelStageConfig stageData)
        {
            _cardData.Clear();

            foreach (var card in stageData.CardPack.Cards)
            {
                _cardData.Add(card);
            }

            foreach (var card in _cards)
            {
                SetRandomCardData(card);
            }
        }

        private void CreateCard(LevelStageConfig stageData)
        {
            var cardToCreate = stageData.Rows * stageData.Columns - _cards.Count;
            var cards = _cardPackFactory.CreateCardPack(stageData.CardPack, cardToCreate);

            for (int i = 0; i < cards.Count; i++)
            {
                _cards.Add(cards[i]);
            }
        }

        private void SetRandomCardData(Card.Card card)
        {
            var dataIndex = Random.Range(0, _cardData.Count);
            card.Init(_cardData[dataIndex].ItemImage, _cardData[dataIndex].Name);
            _cardData.RemoveAt(dataIndex);
        }
    }
}