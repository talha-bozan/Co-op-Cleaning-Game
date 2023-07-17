using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Managers;

public class CityTrashBin : MonoBehaviour
{
    private GameObject _trashBinlid;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject trashBagPrefab;
    private GameObject[] _trashPoolArray;
    private int _trashPoolCount = 50;
    private Transform _trashParent;
    public ScoreUi scoreUi;


    private Vector3[] _stackPositions;
    private int _positionIndex;
    private Vector3[] _movementArray;
    private Vector3[] _samplePositions;
    
    private Transform _positionParent;


    //FillBar
    private GameObject _fillBarObject;
    private Material _fillBarMaterial;
    private int _fillAmountHash = Shader.PropertyToID("_FillAmount");
    private float _fillAmount;



    [SerializeField] GameObject truckObject;
    private bool isTruckArrived = false;
    public bool _trashIsFull = false;
    private int _trashCount = 0;
    
    void Start()
    {
        CacheFillVariables();
        OpenLid();
        GenerateTrashPool();
        GeneratePositions();
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        EventManager.Instance.ONCityTrashIsFull += ONCityTrashIsFull;
    }

    private void ONCityTrashIsFull(bool isFull)
    {
        if (isFull) return;
        _trashCount = 0;
        _positionIndex = 0;
        foreach (var trash in _trashPoolArray)
        {
            trash.SetActive(false);
        }
        _trashIsFull = false;
        SetFillAmount();
    }

    #region FillBar

    private void CacheFillVariables()
    {
        _fillBarObject = transform.GetChild(3).gameObject;
        _fillBarMaterial = _fillBarObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial;
        _trashCount = 0;
        SetFillAmount();
    }

    private void SetFillAmount()
    {
        _fillAmount = (float)_trashCount / (float)_trashPoolCount;
        _fillBarMaterial.SetFloat(_fillAmountHash, _fillAmount);
    }

    #endregion

    #region Lid Movement

    private void OpenLid()
    {
        _trashBinlid = transform.GetChild(1).gameObject;
        LeanTween.rotateAroundLocal(_trashBinlid, Vector3.right, 75f, 2f).setEase(LeanTweenType.easeOutQuad).setOnComplete(()=>_fillBarObject.SetActive(true));
    }
    private void CloseLid()
    {
        
        LeanTween.rotateAroundLocal(_trashBinlid, Vector3.right, -75f, 2f).setEase(LeanTweenType.easeOutQuad);
    }

    #endregion


    #region Trash/Position Generation


    private void GenerateTrashPool()
    {
        _trashPoolArray = new GameObject[_trashPoolCount];
        _trashParent = transform.GetChild(0);
        for (int i = 0; i < _trashPoolCount; i++)
        {
            var trash = Instantiate(trashBagPrefab, _trashParent);
            trash.transform.localScale = new Vector3(.3f, .3f, .3f);
            trash.gameObject.SetActive(false);
            _trashPoolArray[i] = trash;
        }
    }

    private void GeneratePositions()
    {
        _positionParent = transform.GetChild(0);
        _samplePositions = new Vector3[10];
        _stackPositions = new Vector3[50];
        //int stackIndex;
        for (int i = 2; i < 12; i++)
        {
            _samplePositions[i - 2] = _positionParent.GetChild(i).position;
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var sp = _samplePositions[j];
                sp = new Vector3(sp.x, sp.y + (i * .15f), sp.z);
                _stackPositions[(i * 10 + j)] = sp;
            }
        }

        _movementArray = new Vector3[4];
        
        _movementArray[1] = _positionParent.GetChild(1).position;
        _movementArray[2] = _positionParent.GetChild(0).position;
    }

    #endregion

    private void sendTruckToTheBin() {
        LeanTween.move(truckObject, new Vector3(0.05f, 0.15f, -4.07f),2f);    
    }
    private void sendTruckToTheBegginning()
    {
        LeanTween.move(truckObject, new Vector3(-14.74f, 0.15f, -4.07f), 2f);
    }

    public bool CheckForSpace()
    {
        return _trashCount < _trashPoolCount;
    }

    public void SendTrashToTheBin(Vector3 position)
    {
        if (_trashIsFull) return;
        _trashCount += 1;

        SetFillAmount();
        
        var lp = _positionParent.InverseTransformPoint(position);
        _movementArray[0] = position;
        _movementArray[3] = _stackPositions[_positionIndex];
        
        _trashPoolArray[_positionIndex].transform.position = lp;
        _trashPoolArray[_positionIndex].SetActive(true);
        var rndRotation = Random.rotation.eulerAngles;
        rndRotation = new Vector3(rndRotation.x + 1080f, rndRotation.y, rndRotation.z + 1080f);
        LeanTween.move(_trashPoolArray[_positionIndex], _movementArray, .5f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotateLocal(_trashPoolArray[_positionIndex], rndRotation, .5f);
        _positionIndex += 1;

        if(_trashCount >= _trashPoolCount)
        {
            Debug.Log("TrashIsFull");
            EventManager.Instance.OnONCityTrashIsFull(true);
            _trashIsFull = true;
        }
    }
    public void ReFill()
    {
        if (isTruckArrived)
        {

            for (int i = 0; i < _trashPoolArray.Length; i++)
            {
                _trashPoolArray[i].SetActive(false);
            }
            OpenLid();
            GenerateTrashPool();
            GeneratePositions();
            isTruckArrived = false;
            _trashIsFull = false;
            _slider.value = 0;
        }
    }
    IEnumerator WaitToFill()
    {
        sendTruckToTheBin();
        yield return new WaitForSeconds(3f);
        CloseLid();
        yield return new WaitForSeconds(2f);
        isTruckArrived = true;
        ReFill();
        sendTruckToTheBegginning();
        yield return new WaitForSeconds(3f);
        truckObject.transform.position = new Vector3(8.15f, 0.15f, -4.07f);
        
    }



    }
