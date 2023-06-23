using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FailScreenController : MonoBehaviour
    {
        
       
        private CanvasGroup _canvasGroup;
        private RectTransform _background;
        private RectTransform _shine;
        private RectTransform _ribbonFrame;
        private RectTransform _tryAgainButton;
        private RectTransform _cryingEmoji;
        
        
        
        private List<RectTransform> _rectList;

        private bool _isReloadPressed;
        void Start()
        {
            PopulateObjects();
            EventManager.Instance.ONLevelEnd += ONLevelEnd;
             //SubscribeIronsourceCallbacks();
        }
        
        private void PopulateObjects()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _background = transform.GetChild(0).GetComponent<RectTransform>();
            _shine = transform.GetChild(3).GetComponent<RectTransform>();
            
            _ribbonFrame = transform.GetChild(1).GetComponent<RectTransform>();
            _tryAgainButton = transform.GetChild(2).GetComponent<RectTransform>();
            _cryingEmoji = transform.GetChild(4).GetComponent<RectTransform>();
            
            _tryAgainButton.GetComponent<Button>().onClick.AddListener(ReloadLevel);

            _rectList = new List<RectTransform>()
            {
                _background,
                _ribbonFrame,
                _tryAgainButton,
                _cryingEmoji,
                _shine
            };
            
        }

/* Don't Uncomment before installing IronSource SDK

#region IronSource
        private void SubscribeIronsourceCallbacks()
        {
        
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        }
        private void UnSubscribeIronsourceCallbacks()
        {
            
            IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;
        }

        #region IronSourceCallBacks
        
        
    

        private void RewardedVideoAdShowFailedEvent(IronSourceError obj)
        {
            UnSubscribeIronsourceCallbacks();
            if(!_isReloadPressed)return;
            GameManager.Instance.ReloadLevel();
        
        }


        private void RewardedVideoAdClosedEvent()
        {
            UnSubscribeIronsourceCallbacks();
            if(!_isReloadPressed)return;
            GameManager.Instance.ReloadLevel();
        }
     
#endregion
*/



        private void ONLevelEnd(bool isSuccess)
        {
            if (isSuccess) return;
            ShowFailScreen();
        }

        

       
        private void ShowFailScreen()
        {
            AudioManager.Instance.PlayFailMusic();
            foreach (var rectTransform in _rectList)
            {
                rectTransform.gameObject.SetActive(true);
            }

            StartCoroutine(AlphaRoutine());

        }

        IEnumerator AlphaRoutine()
        {
            var counter = 0f;
            while (counter<=.5f)
            {
                _canvasGroup.alpha = Easings.QuadEaseOut(counter, 0f, 1f, .5f);
                counter += Time.deltaTime;
                yield return null;
            }

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            ResumeFailScreen();
        }

        private void ResumeFailScreen()
        {
            StartCoroutine(ResumeRoutine());
        }
        
        IEnumerator ResumeRoutine()
        {
            var counter = 0f;
            
            
            while (counter<=.5f)
            {
                var t = Easings.easeOutBack(0f,1f,counter/.5f);
                var s = Mathf.LerpUnclamped(0f, 1f,t);
                _ribbonFrame.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                yield return null;
            }

            counter = 0f;
            
            while (counter<=.25f)
            {
                var t = Easings.QuadEaseOut(counter, 0f, 1f, .25f);
                _cryingEmoji.localScale = new Vector3(t, t, t);
                counter += Time.deltaTime;
                yield return null;
            }

            counter = 0f;
            yield return new WaitForSeconds(.35f);
            
            _shine.gameObject.SetActive(true);
            while (counter<=.5f)
            {
                var t = Easings.easeOutBack(0f,1f,counter/.5f);
                var s = Mathf.LerpUnclamped(0f, 1f,t);
                _tryAgainButton.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                
                yield return null;
            }
        }
        
        

        private void ReloadLevel()
        {
            AudioManager.Instance.PlayButtonClick();
            _tryAgainButton.GetComponent<Button>().onClick.RemoveListener(ReloadLevel); 
            GameManager.Instance.ReloadLevel();
            
#if UNITY_ANDROID && !UNITY_EDITOR
            Vibration.VibratePop();
#elif UNITY_IOS
            Vibration.VibratePop();
#endif

/* Don't Uncomment before installing IronSource SDK

            _isReloadPressed = true;
            if (PlayerPrefs.HasKey("failCount"))
            {
                var f = PlayerPrefs.GetInt("failCount");
                f += 1;
                f %= 2;
                PlayerPrefs.SetInt("failCount",f);
                if (f == 0)
                {
                    
                    if (IronSource.Agent.isRewardedVideoAvailable())
                    {
                        IronSourceController.Instance.ShowRewardedAd();
                        
                    }else
                    {
                        GameManager.Instance.ReloadLevel();
                    }
                    
                }else
                {
                    GameManager.Instance.ReloadLevel();
                }
            }
            else
            {
                PlayerPrefs.SetInt("failCount",1);
                GameManager.Instance.ReloadLevel();
            }
            
*/

        }
        
        

       
    }
}
