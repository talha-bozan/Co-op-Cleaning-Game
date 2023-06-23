using UnityEngine;
using UnityEngine.SceneManagement;
using Managers;
//using GameAnalyticsSDK;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class AnalyticsTracking : MonoBehaviour
{
    private Stopwatch _stopWatch = new Stopwatch();
    private int _levelTime;
    private float _time2;
    private int _moveCount;
    private int _levelIndex;
    private int _levelNumber;
    private bool _canCount;
    private int _attemptCount;

    private string _completionTime;
    
  /*  
    void Start()
    {
        RegisterEvents();
       OnLevelStart();
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        _levelNumber = _levelIndex - 2;
        PlayerPrefs.SetInt("LastLevel", _levelIndex);
        if (!PlayerPrefs.HasKey("FirstPlay"+"00"+ _levelNumber))
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,"Level"+"00"+(_levelNumber).ToString());
            PlayerPrefs.SetInt("FirstPlay"+"00"+ _levelNumber,1);
        }
        
        if(PlayerPrefs.HasKey("Attempt Level00"+_levelNumber)){
            _attemptCount = PlayerPrefs.GetInt("Attempt Level00"+_levelNumber);
            _attemptCount += 1;
            GameAnalytics.NewDesignEvent("Attempt Level00"+(_levelNumber).ToString(), _attemptCount);
            PlayerPrefs.SetInt("Attempt Level00"+_levelNumber, _attemptCount);
        }
        else
        {
            _attemptCount += 1;
            GameAnalytics.NewDesignEvent("Attempt Level00"+(_levelNumber).ToString(), _attemptCount);
            PlayerPrefs.SetInt("Attempt Level00"+_levelNumber, _attemptCount);
        }
        
        
    }

    private void RegisterEvents(){
        EventManager.Instance.ONBattleEnded += ONBattleEnded;
    }

    private void ONBattleEnded(bool hasWin)
    {
        if (hasWin)
        {
            OnLevelComplete();
            
        }
        else
        {
            OnLevelFail();
            
        }
        
    }

    private void OnLevelReload()
    {
        if(_stopWatch.IsRunning){
            _stopWatch.Stop();
            var ft = _stopWatch.Elapsed;
            
        }

        
        //_attemptCount += 1;
        //PlayerPrefs.SetInt("Attempt Level00"+_levelIndex, _attemptCount);

        GameAnalytics.NewDesignEvent("Reload-Level"+"00"+(_levelIndex).ToString(), _levelTime);
        //GameAnalytics.NewDesignEvent("Reload-MoveLevel"+"00"+(_levelIndex).ToString(), _moveCount);
        
        GameManager.Instance.ReloadLevel();


    }

   
    private void OnLevelComplete()
    {
        _canCount = false;
        _stopWatch.Stop();
        var ft = _stopWatch.Elapsed;
        var t = (float)ft.TotalSeconds;
        _levelTime = Mathf.RoundToInt(t);
        var order = _levelIndex - 3;
        PlayerPrefs.SetInt("LastComplete", order);
        if (!PlayerPrefs.HasKey("FirstComplete" + "00" + _levelNumber))
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,"Level"+"00"+(_levelNumber).ToString(),_levelTime);
            PlayerPrefs.SetInt("FirstComplete" + "00" + _levelNumber,1);
        }
        
        //GameAnalytics.NewDesignEvent("Win_Level"+"00"+(_levelIndex).ToString(), _levelTime);
        
        
    }
    
    private void OnLevelFail()
    {
        _canCount = false;
        _stopWatch.Stop();
        var ft = _stopWatch.Elapsed;
        var t = (float)ft.TotalSeconds;
        _levelTime = Mathf.RoundToInt(t);
        
        
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail,"Level"+"00"+(_levelNumber).ToString(),_levelTime);
        //GameAnalytics.NewDesignEvent("Fail_Level"+"00"+(_levelIndex).ToString(), _levelTime);
        
        
    }

    private void OnLevelStart()
    {
        _canCount = true;
        _stopWatch.Start();
    }
*/

    
}
