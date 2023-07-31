using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class CharacterCollection2 : MonoBehaviour
{

    private int _userId;
    private bool _hasOpened;
    private CityTrashBin _cityTrashBin;
    private float _trashReleaseFrequency = .5f;
    private float _nextRelease;
    private bool _startCount;
    [SerializeField] private CharacterTrashBin2 _selfTrashBin;
    [SerializeField] private VacuumCleaner _vacuumCleaner;
    private bool _canThrow;
    private bool _allTrashThrown;


    public bool AllTrashThrown { get => _allTrashThrown; set => _allTrashThrown = value; }
    public int UserId { get => _userId; }

    private bool _cityTrashIsFull;

    private GameObject _fillBarBgObject;
    private GameObject _fillBarObject;

    private void Start()
    {
        //_selfTrashBin = GetComponentInChildren<CharacterTrashBin2>();
        //_vacuumCleaner = GetComponentInChildren<VacuumCleaner>();
        _canThrow = true;
        EventManager.Instance.ONCityTrashIsFull += ONCityTrashIsFull;
        _fillBarBgObject = transform.GetChild(0).GetChild(2).gameObject;
        _fillBarObject = transform.GetChild(0).GetChild(3).gameObject;
    }

    public void SetUserId(int id)
    {
        _userId = id;
    }

    private void ONCityTrashIsFull(bool isFull)
    {
        if (isFull) return;
        _cityTrashIsFull = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<RoomController>(out RoomController room))
        {

            if (room.RoomId != _userId)
            {
                _vacuumCleaner.WrongRoom = true;
                return;
            }
            _vacuumCleaner.WrongRoom = false;
            _hasOpened = !_hasOpened;
            _vacuumCleaner.ActivatePullEffect(_hasOpened);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.TryGetComponent<CityTrashBin>(out _cityTrashBin))
        {
            if (_allTrashThrown || _cityTrashIsFull) return;
            if (_canThrow)
            {
                if (!_cityTrashBin.CheckForSpace())
                {
                    _cityTrashIsFull = true;
                }
                if (_selfTrashBin.GetTrashCount() == 0)
                {
                    _allTrashThrown = true;
                    return;
                }
                _nextRelease = Time.time + _trashReleaseFrequency;
                _canThrow = false;
                _cityTrashBin.SendTrashToTheBin(_selfTrashBin.GetReleasePosition());
                _vacuumCleaner.DecreaseCollectedCount();
                EventManager.Instance.OnONGarbageDumped(_userId);
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


    public void ActivateFillBars(bool isActive)
    {
        _fillBarObject.SetActive(isActive);
        _fillBarBgObject.SetActive(isActive);
    }

}
