using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Managers;
using System;

public class ScoreBoard : MonoBehaviour
{
    private int _userId;
    private int _fullCapacity=144;
    private int _collectedAmount;


    private TextMeshProUGUI _scoreText;
    private Slider _progressbar;

    void Start()
    {
        _userId = transform.GetSiblingIndex();
        _scoreText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _progressbar = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        SetScore();
        EventManager.Instance.ONGarbageDumped += ONGarbageDumped;
    }

    private void ONGarbageDumped(int userId)
    {
        if(_userId != userId)return;
        _collectedAmount += 1;
        SetScore();
    }

    private void SetScore(){
        _scoreText.text = _collectedAmount+"/"+_fullCapacity;
        _progressbar.value = (float)_collectedAmount/(float)_fullCapacity;

        if(_collectedAmount == _fullCapacity){
            EventManager.Instance.OnONAllTrashCleaned(_userId);
        }
    }

   
}
