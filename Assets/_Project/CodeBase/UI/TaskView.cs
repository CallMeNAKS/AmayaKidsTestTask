using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _taskDescriptionText;

        private CanvasGroup _canvasGroup;
        
        private const float FadeDuration = 2f;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _canvasGroup.alpha = 0;
        }

        public void ShowTask(string taskDescription, string taskAnswer)
        {
            _taskDescriptionText.text = taskDescription + " " + taskAnswer;

            _canvasGroup.DOFade(1f, FadeDuration).SetEase(Ease.InOutQuad);
        }
    }
}