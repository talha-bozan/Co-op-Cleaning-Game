using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class CharacterTrashBin : MonoBehaviour
{
    private Vector3 _trashBagStartPosition;
    private Vector3[] _trashBagPositions;
    private TrashBagController[] _trashBags;
    private int _trashIndex;
    private int _trashCount;
    private CharacterCollection _characterCollection;

    private void Start()
    {
        CacheObjects();
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        EventManager.Instance.ONTrashCollected += ONTrashCollected;
    }

    

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
    }

    #region event Callbacks
    private void ONTrashCollected()
    {
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
        //Invoke(nameof(DecreaseIndex), .1f);
        return _trashBags[_trashIndex].transform.position;
    }

    private void DecreaseIndex()
    {
        if (_trashIndex == 0) return;
        
    }

    

}
