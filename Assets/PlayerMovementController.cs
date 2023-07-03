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
    private float JumpHeight = 3.0f;
    private float Gravity = -9.81f;
    private Vector3 jumpVelocity;

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
        if (characterController.isGrounded && jumpVelocity.y < 0f)
        {
            jumpVelocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            jumpVelocity.y = Mathf.Sqrt(-2 * JumpHeight * Gravity);
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ);

        // If the shift key is down, then dash
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(move * 2 * Speed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * Speed * Time.deltaTime);
        }

        jumpVelocity.y += 2.3f * Gravity * Time.deltaTime;

        characterController.Move(jumpVelocity * Time.deltaTime);
    }


    public void SetPosition()
    {
        transform.position = new Vector3(Random.Range(-5, 5), 0.8f, Random.Range(-15, 7));
    }
}
