using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class VacuumCleaner : MonoBehaviour
{
    private int _totalCapacity = 20;
    private int _collectedTrashAmount;
    private bool _wrongRoom;
    private GameObject _pullEffect;

    public bool WrongRoom { get => _wrongRoom; set => _wrongRoom = value; }

    private void Start()
    {
        _pullEffect = transform.GetChild(0).gameObject;
    }

    public void ActivatePullEffect(bool isActive)
    {
        _pullEffect.SetActive(isActive);
    }

    public void DecreaseCollectedCount()
    {  
        _collectedTrashAmount -= 1;
    }

    public bool CheckIfThereIsSpace()
    {
        if (_wrongRoom) return false;
        if (_collectedTrashAmount < _totalCapacity)
        {  
            _collectedTrashAmount += 1;           
            //Debug.Log($"Total trash: {_collectedTrashAmount}");
            return true;
        }
        return false;
    }

    
}
