using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityTrashBin : MonoBehaviour
{
    private GameObject _trashBinlid;

    [SerializeField] private GameObject trashBagPrefab;
    private GameObject[] _trashPoolArray;
    private int _trashPoolCount = 50;
    private Transform _trashParent;

    private Vector3[] _stackPositions;
    private int _positionIndex;
    private Vector3[] _movementArray;
    private Vector3[] _samplePositions;

    private Transform _positionParent;
    
    void Start()
    {
        OpenLid();
        GenerateTrashPool();
        GeneratePositions();
    }


    private void OpenLid()
    {
        _trashBinlid = transform.GetChild(1).gameObject;
        LeanTween.rotateAroundLocal(_trashBinlid, Vector3.right, 75f, 2f).setEase(LeanTweenType.easeOutQuad);
    }

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
        int stackIndex;
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



    public void SendTrashToTheBin(Vector3 position)
    {
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
    }

    
}
