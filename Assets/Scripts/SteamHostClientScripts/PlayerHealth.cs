using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    private int health = 4;
    [SerializeField] private LayerMask pLayerMask;

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            DetectPlayers();
        }
    }

    void DetectPlayers()
    {
        Collider[] playersInRange = Physics.OverlapSphere(this.transform.position, 2f, pLayerMask);

        foreach (Collider player in playersInRange)
        {
            if (player.gameObject == this.gameObject)
                continue;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                CmdHandleDetection(player.gameObject);
            }
        }
    }

    [Command]
    void CmdHandleDetection(GameObject detectedPlayer)
    {
        Debug.Log("Handler");
        PlayerHealth playerScript = detectedPlayer.GetComponent<PlayerHealth>();
        if (playerScript != null)
        {
            playerScript.TakeDamage();
        }
    }

    void TakeDamage()
    {
        health--;
        Debug.Log("Player health: " + health);

        // You can add any other logic here, e.g., checking if the player is dead and handling that case.
    }
}


