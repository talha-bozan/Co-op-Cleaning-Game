using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollection : MonoBehaviour
{
    [SerializeField] private GameObject pullEffect;
    private bool _hasOpened;
    private CityTrashBin _cityTrashBin;
    private float _trashReleaseFrequency=.5f;
    private float _nextRelease;
    private bool _startCount;
    private CharacterTrashBin _selfTrashBin;
    private VacuumCleaner _vacuumCleaner;
    private bool _canThrow;
    private bool _allTrashThrown;
    [SerializeField] private CityTrashBin _trashBin;

    public bool AllTrashThrown { get => _allTrashThrown; set => _allTrashThrown = value; }

    private void Start()
    {
        _selfTrashBin = GetComponentInChildren<CharacterTrashBin>();
        _vacuumCleaner = GetComponentInChildren<VacuumCleaner>();
        _canThrow = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RoomDoor"))
        {
            _hasOpened = !_hasOpened;
            pullEffect.SetActive(_hasOpened);
        }
    }

    private void OnTriggerStay(Collider other)
    {   if (_trashBin.stopSending)
            return;
        
        if(other.TryGetComponent<CityTrashBin>(out _cityTrashBin))
        {
            if (_allTrashThrown) return;
            if (_canThrow)
            {
                if(_selfTrashBin.GetTrashCount() == 0)
                {
                    _allTrashThrown = true;
                    return;
                }
                _nextRelease = Time.time + _trashReleaseFrequency;
                _canThrow = false;
                _cityTrashBin.SendTrashToTheBin(_selfTrashBin.GetReleasePosition());
                _vacuumCleaner.DecreaseCollectedCount();
            }
                
            
        }
    }

    private void Update()
    {
        if (!_allTrashThrown)
        {
            if (!_canThrow)
            {
                if (Time.time < _nextRelease) return;
                _canThrow = true;
            }
        }
    }

}
