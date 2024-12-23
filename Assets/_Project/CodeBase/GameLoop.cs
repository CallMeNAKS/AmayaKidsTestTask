using System.Collections;
using System.Collections.Generic;
using _Project.CodeBase.Data;
using _Project.CodeBase.QuizMechanics;
using _Project.CodeBase.UI;
using _Project.CodeBase.Utils;
using _Project.CodeBase.VFX;
using CodeBase.Card;
using CodeBase.Level;
using Zenject;

namespace _Project.CodeBase
{
    public class GameLoop : IInitializable, ILateDisposable
    {
        private readonly CardCreator _cardCreator;
        private readonly TaskView _taskView;
        private readonly UniqueAnswerService _uniqueAnswerService;
        private readonly AnswerListener _answerListener;
        private readonly LevelData _levelData;
        private readonly Coroutines _coroutines;
        private readonly EndGameView _endGameView;
        private readonly LoadingView _loadingView;

        private List<Card> _currentCards = new List<Card>();
        private string _currentAnswer;
        private int _levelIndex = 0;

        private const float CardBounceDuration = 1f;
        private const int Bounciness = 1;


        public GameLoop(CardCreator cardCreator,
            TaskView taskView,
            UniqueAnswerService uniqueAnswerService,
            AnswerListener answerListener,
            IDataService dataService,
            Coroutines coroutines,
            LoadingView loadingView,
            EndGameView endGameView)
        {
            _cardCreator = cardCreator;
            _taskView = taskView;
            _uniqueAnswerService = uniqueAnswerService;
            _answerListener = answerListener;
            _levelData = dataService.LevelData;
            _coroutines = coroutines;
            _endGameView = endGameView;
            _loadingView = loadingView;
        }

        public void Initialize()
        {
            StartLevel();
            AnimateCards();
        }

        public void LateDispose()
        {
            _answerListener.CorrectlyAnswered -= StartNextLevel;
            _endGameView.OnRestartButtonPressed -= RestartGame;
        }

        private void StartLevel()
        {
            if (_levelIndex >= _levelData.Stages.Length)
            {
                FinishGame();
                return;
            }

            _currentCards = _cardCreator.CreateCards(_levelData.Stages[_levelIndex]);
            _currentAnswer = _uniqueAnswerService.GetAnswer(_currentCards);

            _taskView.ShowTask(_levelData.Stages[_levelIndex].CardPack.QuestDescription, _currentAnswer);
            _answerListener.Init(_currentCards, _currentAnswer);

            _answerListener.CorrectlyAnswered += StartNextLevel;
            _levelIndex++;
        }

        private void AnimateCards()
        {
            foreach (var card in _currentCards)
            {
                _coroutines.StartCoroutine(Effects.Bounce(card.transform, CardBounceDuration, Bounciness));
            }
        }

        private void StartNextLevel(Card obj)
        {
            _answerListener.CorrectlyAnswered -= StartNextLevel;
            StartLevel();
        }

        private void FinishGame()
        {
            _endGameView.gameObject.SetActive(true);
            _endGameView.OnRestartButtonPressed += RestartGame;
        }

        private void RestartGame()
        {
            _endGameView.gameObject.SetActive(false);
            _endGameView.OnRestartButtonPressed -= RestartGame;

            _coroutines.StartCoroutine(WaitForRestart());
        }

        private IEnumerator WaitForRestart()
        {
            yield return _coroutines.StartCoroutine(_loadingView.FadeInEffect());

            _cardCreator.Reset();
            _levelIndex = 0;

            Initialize();

            yield return _coroutines.StartCoroutine(_loadingView.FadeOutEffect());
        }
    }
}