using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class testtttt : NetworkBehaviour
{

    [SyncVar] public int health = 4;

    // Update is called once per frame
    void Update()
    {
        
    }



    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other == this.gameObject) { return; }

            NetworkServer.Destroy(other.gameObject);


        }
    }
}
