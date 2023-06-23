using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {

        private int _sceneCount;
        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            _sceneCount = SceneManager.sceneCount;
#if UNITY_ANDROID && !UNITY_EDITOR
            Vibration.Init();
#elif UNITY_IOS
            Vibration.Init();
#endif
        }


        public void OpenNextLevel()
        {
            EventManager.Instance.NextLevelReset();
            
            var index = SceneManager.GetActiveScene().buildIndex;
            index += 1;
            index %= _sceneCount;
            if (index == 0)
            {
                index = 1;
            }
            SceneManager.LoadScene(index);
        }

        public void ReloadLevel()
        {
            EventManager.Instance.NextLevelReset();
            
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }
}
