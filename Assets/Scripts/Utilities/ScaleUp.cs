using System;
using UnityEngine;

namespace Utilities
{
    public class ScaleUp : MonoBehaviour
    {
        private LTDescr _scaleTween;
        private static readonly Vector3 ScaleUpScale = new Vector3(1.2f, 1.2f, 1.2f);
        private void OnEnable()
        {
            Invoke(nameof(StartTween),2.3f);
        }


        private void StartTween()
        {
            var rect = GetComponent<RectTransform>();
            _scaleTween = LeanTween.scale(rect, ScaleUpScale, 1f).setEase(LeanTweenType.easeOutElastic).setLoopPingPong(100);
        }

        private void OnDisable()
        {
            _scaleTween.pause();
            _scaleTween.reset();
        }
    }
}
