using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private Image _panelImage;
        [SerializeField] private RestartButton _restartButton;

        private const float FadeValue = 0.6f;
        private const float FadeDuration = 1f;

        public event Action OnRestartButtonPressed;


        private void OnEnable()
        {
            Color color = _panelImage.color;
            color.a = 0;
            _panelImage.color = color;

            _restartButton.OnClick += Restart;
            _panelImage.DOFade(FadeValue, FadeDuration);
        }

        private void OnDisable()
        {
            _restartButton.OnClick -= Restart;
        }

        private void Restart()
        {
            OnRestartButtonPressed?.Invoke();
        }
    }
}