using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class testtttt : NetworkBehaviour
{
    [SyncVar] public int health = 4;
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
            if (player.gameObject == this.gameObject) // Skip over the "owner" player
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

        testtttt playerScript = detectedPlayer.GetComponent<testtttt>();
        if (playerScript != null)
        {
            playerScript.health--;
        }
    }
}
