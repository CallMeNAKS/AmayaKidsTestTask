using System.Collections.Generic;
using System.Linq;
using CodeBase.Card;
using UnityEngine;

namespace _Project.CodeBase.QuizMechanics
{
    public class UniqueAnswerService
    {
        private List<string> _answers = new List<string>();
        private HashSet<string> _previousAnswers = new HashSet<string>();

        public string GetAnswer(List<Card> cards)
        {
            _answers.Clear();

            foreach (var card in cards)
            {
                _answers.Add(card.CardName);
            }

            var validAnswers = _answers.Where(answer => !_previousAnswers.Contains(answer)).ToList();

            if (!validAnswers.Any())
            {
                _previousAnswers.Clear();
                validAnswers = _answers;
            }

            var answer = validAnswers[Random.Range(0, validAnswers.Count)];
            _previousAnswers.Add(answer);

            return answer;
        }
    }
}