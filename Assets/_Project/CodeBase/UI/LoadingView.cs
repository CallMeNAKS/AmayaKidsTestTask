using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Image _loadImage;

        private const float FadeInDuration = 1f;
        private const float FadeOutDuration = 2f;



        private void Awake()
        {
            _loadImage.enabled = false;
        }

        public IEnumerator FadeInEffect()
        {
            _loadImage.enabled = true;
            yield return _loadImage.DOFade(1, FadeInDuration).WaitForCompletion();
        }

        public IEnumerator FadeOutEffect()
        {
            yield return _loadImage.DOFade(0, FadeOutDuration).WaitForCompletion();
            _loadImage.enabled = false;
        }
    }
}