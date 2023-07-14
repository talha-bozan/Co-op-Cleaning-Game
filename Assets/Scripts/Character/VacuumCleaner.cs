using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VacuumCleaner : MonoBehaviour
{
    private int _totalCapacity = 20;
    public int _collectedTrashAmount;
    [SerializeField] private Slider _trashBinSlider;

    private void Start()
    {
        _trashBinSlider.minValue = 0;
        _trashBinSlider.maxValue = _totalCapacity - 1;
    }

    public void DecreaseCollectedCount()
    {  
        _collectedTrashAmount -= 1;
        _trashBinSlider.value = _collectedTrashAmount;
    }

    public bool CheckIfThereIsSpace()
    {
        if (_collectedTrashAmount < _totalCapacity)
        {  
            _collectedTrashAmount += 1;
            _trashBinSlider.value = _collectedTrashAmount;
            Debug.Log($"Total trash: {_collectedTrashAmount}");
            return true;
        }
        return false;
    }

    
}
