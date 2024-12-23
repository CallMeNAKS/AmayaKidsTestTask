using System;
using System.Collections;
using System.Collections.Generic;
using _Project.CodeBase.Utils;
using _Project.CodeBase.VFX;
using CodeBase.Card;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.QuizMechanics
{
    public class AnswerListener : ILateDisposable
    {
        private readonly Coroutines _coroutines;
        private readonly Particle _particle;

        private List<Card> _cards = new List<Card>();
        private string _answer;

        private const float BounceDuration = 1f;
        private const int Bounciness = 2;
        private const float ShakeDuration = 2f;
        private const int Shakes = 2;

        public event Action<Card> CorrectlyAnswered;

        public AnswerListener(Coroutines coroutines,
            Particle particle)
        {
            _coroutines = coroutines;
            _particle = particle;
        }

        public void Init(List<Card> cards, string answer)
        {
            _cards = cards;
            _answer = answer;

            foreach (var card in _cards)
            {
                card.OnClick += CardOnOnClick;
            }
        }

        public void LateDispose()
        {
            foreach (var card in _cards)
            {
                card.OnClick -= CardOnOnClick;
            }
        }

        private void CardOnOnClick(Card card)
        {
            if (card.CardName == _answer)
            {
                _coroutines.StartCoroutine(OnCorrectAnswer(card));
            }
            else
            {
                _coroutines.StartCoroutine(Effects.ShakeHorizontally(card.CardImage.transform, ShakeDuration, Shakes));
            }
        }

        private IEnumerator OnCorrectAnswer(Card card)
        {
            _particle.StarParticle(card.transform.position);
            
            yield return _coroutines.StartCoroutine(
                Effects.Bounce(card.CardImage.transform, BounceDuration, Bounciness));

            LateDispose();
            CorrectlyAnswered?.Invoke(card);
        }
    }
}