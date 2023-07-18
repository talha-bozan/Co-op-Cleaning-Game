using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class TrashController : MonoBehaviour
{

    private int _roomId;

    private void Start() {
       Invoke(nameof(GetRoomId),.25f);
    }

    private void GetRoomId(){
        _roomId = GetComponentInParent<RoomController>().RoomId;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<VacuumCleaner>(out VacuumCleaner vacuum))
        {
            if (!vacuum.CheckIfThereIsSpace()) return;
            EventManager.Instance.OnONTrashCollected(_roomId);
            gameObject.SetActive(false);
        }
    }



}
