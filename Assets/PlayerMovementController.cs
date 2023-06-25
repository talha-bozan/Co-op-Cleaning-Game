using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerMovementController : NetworkBehaviour
{
    private float Speed = 10.0f;
    public GameObject PlayerModel;
    private CharacterController characterController;

    private void Start()
    {
        PlayerModel.SetActive(false);
        characterController = GetComponent<CharacterController>(); // assign the CharacterController to characterController variable
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "newgamescene")
        {
            if (PlayerModel.activeSelf == false)
            {
                SetPosition();
                PlayerModel.SetActive(true);
            }
            if (isOwned)
            {
                Movement();
            }
        }
    }

    public void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX * Speed, 0, moveZ * Speed);
       // move.Normalize();


        if (characterController != null)
        {
            characterController.Move(move * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Missing CharacterController!");
        }
    }

    public void SetPosition()
    {
        transform.position = new Vector3(Random.Range(-5, 5), 0.8f, Random.Range(-15, 7));
    }
}
