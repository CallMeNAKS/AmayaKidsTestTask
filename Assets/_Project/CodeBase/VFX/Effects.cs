using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.VFX
{
    public class Effects
    {
        private const float BounceScale = 1.1f;
        private const float ShakeOffset = 20f;
        private const float ShrinkScale = 0.8f;

        public static IEnumerator Bounce(Transform transform, float duration, int bounciness)
        {
            float halfDuration = duration / 2;
            Sequence bounceSequence = DOTween.Sequence();

            for (int i = 0; i < bounciness; i++)
            {
                bounceSequence.Append(transform.DOScale(Vector3.one * BounceScale, halfDuration).SetEase(Ease.OutQuad));
                bounceSequence.Append(transform.DOScale(Vector3.one, halfDuration).SetEase(Ease.InQuad));
            }

            yield return bounceSequence.Play().WaitForCompletion();
        }

        public static IEnumerator ShakeHorizontally(Transform transform, float duration, int shakes)
        {
            float singleShakeDuration = duration / shakes;
            Vector3 originalPosition = transform.localPosition;
            Sequence shakeSequence = DOTween.Sequence();

            shakeSequence.Append(transform.DOScale(Vector3.one * ShrinkScale, singleShakeDuration / 2)
                .SetEase(Ease.OutQuad));

            for (int i = 0; i < shakes; i++)
            {
                shakeSequence.Append(transform.DOLocalMoveX(originalPosition.x + ShakeOffset,
                    singleShakeDuration / 2));
                shakeSequence.Append(transform.DOLocalMoveX(originalPosition.x - ShakeOffset,
                    singleShakeDuration / 2));
            }

            shakeSequence.Append(transform.DOLocalMoveX(originalPosition.x, singleShakeDuration));
            shakeSequence.Append(transform.DOScale(Vector3.one, singleShakeDuration / 2).SetEase(Ease.OutQuad));

            yield return shakeSequence.Play().WaitForCompletion();
        }

        // public static IEnumerator StarParticles(Transform transform, float duration)
        // {
        //     
        // }
    }
}