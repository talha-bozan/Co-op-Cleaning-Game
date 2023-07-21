using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerMovementController : NetworkBehaviour
{
    private float Speed = 10.0f;
    public GameObject PlayerModel;
    private Vector3 moveDirection;
    private NavMeshAgent _agent;

    // Add a reference to the PlayerRotationController
    private PlayerRotationController rotationController;

    private void Start()
    {
        PlayerModel.SetActive(false);
        _agent = GetComponent<NavMeshAgent>();

        // Find the PlayerRotationController attached to the player
        rotationController = GetComponent<PlayerRotationController>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "4PlayerGameScene")
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
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ);

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

    }

    public void SetPosition()
    {
        _agent.Warp(new Vector3(Random.Range(-5, 5), 0.8f, Random.Range(-15, 7)));
    }
}
