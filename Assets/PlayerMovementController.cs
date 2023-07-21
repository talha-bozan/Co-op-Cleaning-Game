using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerMovementController : NetworkBehaviour
{
    private float Speed = 10.0f;
    public GameObject PlayerModel;
    private Vector3 moveDirection; // Replace jumpVelocity with moveDirection
    private NavMeshAgent _agent;

    private void Start()
    {
        PlayerModel.SetActive(false);
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "4PlayerGameScene")
        {
            if (PlayerModel.activeSelf == false)
            {
                SetPosition(); // This line is removed as we won't need SetPosition anymore
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
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ);

        // If the shift key is down, then dash
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _agent.Move(move * 2 * Speed * Time.deltaTime);
        }
        else
        {
            _agent.Move(move * Speed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        // Perform your jump logic here
        // For example, you can make the player jump by adding an upward force or animation.
        // Since NavMeshAgent doesn't handle jumping, you need to add your own implementation.
        // For simplicity, you can use transform.position.y to simulate jumping.
    }

    public void SetPosition()
    {
        // Use NavMeshAgent to set the position instead of Random.Range
        _agent.Warp(new Vector3(Random.Range(-5, 5), 0.8f, Random.Range(-15, 7)));
    }
}
