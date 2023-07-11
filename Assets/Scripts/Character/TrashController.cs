using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class TrashController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<VacuumCleaner>(out VacuumCleaner vacuum))
        {
            if (!vacuum.CheckIfThereIsSpace()) return;
            EventManager.Instance.OnONTrashCollected();
            gameObject.SetActive(false);
        }
    }



}
