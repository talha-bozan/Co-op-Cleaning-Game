using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class StartButton : MonoBehaviour
    {
	    private readonly Vector3 _scaleFactor = new Vector3(1.1f, 1.1f, 1.1f);
	    private readonly Vector3 _defaultScale = new Vector3(1f, 1f, 1f);
	    private bool _isGameStarted = false;

		private LTDescr _scaleTween;
		private void Start()
		{
			_scaleTween = LeanTween.scale(gameObject,_scaleFactor,1f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong(100);
			GetComponent<Button>().onClick.AddListener(StartGame);
		}
		
		private void StartGame()
		{
			EventManager.Instance.OnONLevelStart();
			GetComponent<Button>().onClick.RemoveListener(StartGame);
			_scaleTween.pause();
			AudioManager.Instance.PlayButtonClick();
			gameObject.SetActive(false);
		}
		
		
		
    }
}