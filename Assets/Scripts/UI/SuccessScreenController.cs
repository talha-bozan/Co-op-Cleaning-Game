using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SuccessScreenController : MonoBehaviour
    {
       
        private RectTransform _background;
        private RectTransform _ribbonFrame;
        private RectTransform _shine;
        private RectTransform _emoji;
        private RectTransform _continueButton;
        private CanvasGroup _canvasGroup;
        
        //if there will be a money earned animation at the end uncomment lines for Money earned
        //[SerializeField] private TextMeshProUGUI earnedMoneyText;
        //private RectTransform _moneyEarned;

        
        private List<RectTransform> _rectList;
        void Start()
        {
            PopulateObjects();
            EventManager.Instance.ONLevelEnd += OnLevelEnd;
            //SubscribeIronsourceCallbacks();
            //EventManager.Instance.ONMoneyEarned += ONMoneyEarned;
        }
        
        private void PopulateObjects()
        {
            _background = transform.GetChild(0).GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _ribbonFrame = transform.GetChild(2).GetComponent<RectTransform>();
            _shine = transform.GetChild(1).GetComponent<RectTransform>();
            _continueButton = transform.GetChild(3).GetComponent<RectTransform>();
            _emoji = transform.GetChild(4).GetComponent<RectTransform>();
            //_moneyEarned = transform.GetChild(5).GetComponent<RectTransform>();//when you uncomment this line dont forget to add the moneyearned object to rect list

            _continueButton.GetComponent<Button>().onClick.AddListener(OpenNextLevel);
            _rectList = new List<RectTransform>()
            {
                _background,
                _ribbonFrame,
                _continueButton,
                _emoji
            };
            
            
        }


/* Don't Uncomment before installing IronSource SDK

        #region IronSource Funcs
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

        private void RewardedVideoAdShowFailedEvent(IronSourceError obj)
        {
            UnSubscribeIronsourceCallbacks();
            GameManager.Instance.OpenNextLevel();
        
        }


        private void RewardedVideoAdClosedEvent()
        {
            UnSubscribeIronsourceCallbacks();
            GameManager.Instance.OpenNextLevel();
        }
        #endregion
*/



        /*
        private void ONMoneyEarned(int amount)
        {
            earnedMoneyText.text = amount.ToString();
        }
        */



        
        //Level End Event Callback
        private void OnLevelEnd(bool isSuccess)
        {
            if (!isSuccess) return;
            Invoke(nameof(ShowSuccessScreen), 1f);
        }


       
        private void ShowSuccessScreen()
        {
            //Invoke(nameof(ShowTotalAmount),1f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            AudioManager.Instance.PlayWinMusic();
            foreach (var rectTransform in _rectList)
            {
                rectTransform.gameObject.SetActive(true);
            }
            StartCoroutine(ShowSuccessRoutine());
            
            //LeanTween.scale(_moneyEarned, DefaultScale, .5f).setEase(LeanTweenType.easeOutBack).setDelay(moveTime*3.5f).setOnComplete(()=> AudioManager.Instance.PlayMoneyEarned());
        }
        
        IEnumerator ShowSuccessRoutine()
        {
            //Turn on canvas alpha
            var counter = 0f;
            while (counter<=.5f)
            {
                _canvasGroup.alpha = Easings.QuadEaseOut(counter, 0f, 1f, .5f);
                counter += Time.deltaTime;
                yield return null;
            }

            ResumeEndScreen();
            
        }
        
        private void ResumeEndScreen()
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
                _emoji.localScale = new Vector3(t, t, t);
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
                _continueButton.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                
                yield return null;
            }
        }

        private void ShowTotalAmount()
        {
            transform.GetChild(6).gameObject.SetActive(true);
        }

        

        private void OpenNextLevel()
        {
            AudioManager.Instance.PlayButtonClick();  
            _continueButton.GetComponent<Button>().onClick.RemoveListener(OpenNextLevel);
            GameManager.Instance.OpenNextLevel();
            
            
#if UNITY_ANDROID && !UNITY_EDITOR
            Vibration.VibratePop();
#elif UNITY_IOS
            Vibration.VibratePop();
#endif
            
/* Don't Uncomment before installing IronSource SDK

            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSourceController.Instance.ShowRewardedAd();
                
            }else
            {
                GameManager.Instance.OpenNextLevel();
            }
*/

        }
    }
}
