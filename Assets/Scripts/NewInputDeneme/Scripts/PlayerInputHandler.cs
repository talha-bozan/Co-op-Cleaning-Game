using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab;
    MoveControl playerController;
    Vector3 startPos = new Vector3(0, 0, 0);

    void Awake()
    {

        if (playerPrefab != null)
        {
            playerController = GameObject.Instantiate(playerPrefab, startPos, transform.rotation).GetComponent<MoveControl>();
            transform.parent = playerController.transform;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {

        playerController.OnMove(context);
    }
}