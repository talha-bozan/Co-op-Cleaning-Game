using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect : MonoBehaviour
{
    private charMovment _playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerMovement = other.gameObject.GetComponent<charMovment>();

            if (_playerMovement != null)
            {
                _playerMovement.SetMovementEnabled(false);
                StartCoroutine(ReleasePlayerAfterDelay(5));
            }
        }
    }

    private IEnumerator ReleasePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_playerMovement != null)
        {
            _playerMovement.SetMovementEnabled(true);
        }
    }
}
