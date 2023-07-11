using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleaner : MonoBehaviour
{
    private int _totalCapacity = 20;
    private int _collectedTrashAmount;


    public void DecreaseCollectedCount()
    {
        _collectedTrashAmount -= 1;
    }

    public bool CheckIfThereIsSpace()
    {
        if (_collectedTrashAmount < _totalCapacity)
        {
            _collectedTrashAmount += 1;
            Debug.Log($"Total trash: {_collectedTrashAmount}");
            return true;
        }
        return false;
    }

    
}
