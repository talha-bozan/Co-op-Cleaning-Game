using System.Collections;
//using Facebook.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Managers
{
    //********************************************************************************BEWARE Uncomment the lines only After installing the SDK

    
    public class LoadingManager : MonoBehaviour
    {
        
        void Awake ()
        {
            Application.targetFrameRate = 60;
            //InitFacebook();
            StartCoroutine(LoadingRoutine());
        }
/*
        private void InitFacebook()
        {
            if (!FB.IsInitialized) {
                // Initialize the Facebook SDK
                FB.Init(InitCallback);
            } else {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }
        

        private void InitCallback ()
        {
            if (FB.IsInitialized) {
                // Signal an app activation App Event
                FB.Mobile.SetAdvertiserTrackingEnabled(true);
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            } else {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }
        
*/
        private IEnumerator LoadingRoutine()
        {
            
            var index = 2;
            if (PlayerPrefs.HasKey("LastScene"))
            {
                index = PlayerPrefs.GetInt("LastScene");
            }
            yield return new WaitForSeconds(2f);
            /*
            if (!PlayerPrefs.HasKey("FirstPlayStart"))
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,"GameFirstOpen");
                PlayerPrefs.SetInt("FirstPlayStart",1);
            }
            */
            SceneManager.LoadScene(index);
        }
        
    }
    
}
