using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class CharacterTrashBin : MonoBehaviour
{
    private int _userId;
    private Vector3 _trashBagStartPosition;
    private Vector3[] _trashBagPositions;
    private TrashBagController[] _trashBags;
    private int _trashIndex;
    private int _trashCount;
    private int _trashCapacity=20;
    private CharacterCollection _characterCollection;

    //FillBar
    private GameObject _fillBarObject;
    private Material _fillBarMaterial;
    private int _fillAmountHash = Shader.PropertyToID("_FillAmount");
    private float _fillAmount;

    private void Start()
    {
        CacheObjects();
        
        RegisterEvents();
        Invoke(nameof(GetUserId),.25f);
    }

    private void GetUserId(){
        _userId = _characterCollection.UserId;
    }

    private void RegisterEvents()
    {
        EventManager.Instance.ONTrashCollected += ONTrashCollected;
    }

    #region FillBar

    private void CacheFillVariables()
    {
        var userId = _characterCollection.UserId;
        _fillBarMaterial = MaterialDatabase.Instance._fillBarMaterials[userId * 1];
        _characterCollection.transform.GetChild(3).GetComponent<SpriteRenderer>().sharedMaterial = _fillBarMaterial;
        _characterCollection.transform.GetChild(2).GetComponent<SpriteRenderer>().sharedMaterial = MaterialDatabase.Instance._fillBarMaterials[userId * 1+2];
        _trashCount = 0;
        SetFillAmount();
    }

    private void SetFillAmount()
    {
        _fillAmount = (float)_trashCount / (float)_trashCapacity;
        _fillBarMaterial.SetFloat(_fillAmountHash, _fillAmount);
    }

    #endregion



    private void CacheObjects()
    {
        var trashParent = transform.GetChild(0);
        _trashBagStartPosition = trashParent.GetChild(0).transform.localPosition;


        _trashBagPositions = new Vector3[20];
        _trashBags = new TrashBagController[20];
        for (int i = 1; i < 21; i++)
        {
            _trashBagPositions[i-1] = trashParent.GetChild(i).transform.localPosition;
            _trashBags[i-1] = trashParent.GetChild(i).GetComponent<TrashBagController>();
        }

        _characterCollection = GetComponentInParent<CharacterCollection>();
        CacheFillVariables();
    }

    #region event Callbacks
    private void ONTrashCollected(int roomId)
    {
        if(_userId != roomId)return;
        _characterCollection.AllTrashThrown = false;
        Invoke(nameof(ThrowTrashbag), .6f);
    }
    #endregion


    private void ThrowTrashbag()
    {
        _trashCount += 1;
        _trashBags[_trashIndex].transform.localPosition = _trashBagStartPosition;
        _trashBags[_trashIndex].gameObject.SetActive(true);
        _trashBags[_trashIndex].MoveToBin(_trashBagPositions[_trashIndex]);
        _trashIndex += 1;
        SetFillAmount();
    }

    public int GetTrashCount()
    {
        return _trashCount;
    }

    public Vector3 GetReleasePosition()
    {
        _trashIndex -= 1;
        _trashBags[_trashIndex].gameObject.SetActive(false);
        _trashCount -= 1;
        _trashCount = Mathf.Clamp(_trashCount,-1,_trashCapacity-1);
        SetFillAmount();
        //Invoke(nameof(DecreaseIndex), .1f);
        return _trashBags[_trashIndex].transform.position;
    }

    private void DecreaseIndex()
    {
        if (_trashIndex == 0) return;
        
    }

    

}
