using System;
using UnityEngine;

namespace Utilities
{
    public class ScaleUp : MonoBehaviour
    {
        private LTDescr _scaleTween;
        private static readonly Vector3 ScaleUpScale = new Vector3(.8f, .8f, .8f);
        private void OnEnable()
        {
            StartTween();
        }


        private void StartTween()
        {
            
            _scaleTween = LeanTween.scale(gameObject, ScaleUpScale, .5f).setEase(LeanTweenType.easeOutElastic);
        }

        private void OnDisable()
        {
            if (_scaleTween == null) return;
            _scaleTween.pause();
            _scaleTween.reset();
        }
    }
}
