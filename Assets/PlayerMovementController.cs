using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovementController : NetworkBehaviour
{
    private float Speed = 10.0f;
    public GameObject PlayerModel;
    private Vector3 moveDirection;
    private NavMeshAgent _agent;


    public int UserId { get => _userId; }

    private Vector3 _userInput;
    private CharacterCollection2 _collection;
    private CharacterAnim _animation;
    private CharacterAttack2 _attack;

    private bool _characterGotHit;
    private ParticleSystem _getHitParticle;
    private ParticleSystem _smashParticle;

    private bool _levelEnded;
    private int _userId;

    // Add a reference to the PlayerRotationController
    private PlayerRotationController rotationController;

    private void Start()
    {
        

        PlayerModel.SetActive(false);
        _getHitParticle = transform.GetChild(0).GetChild(4).GetComponent<ParticleSystem>();
        _smashParticle = transform.GetChild(0).GetChild(5).GetComponent<ParticleSystem>();
        _agent = GetComponent<NavMeshAgent>();
        _attack = GetComponent<CharacterAttack2>();
        _animation = GetComponent<CharacterAnim>();
        _collection = GetComponent<CharacterCollection2>();

        Invoke(nameof(GetUserId), .25f);


        // Find the PlayerRotationController attached to the player
        rotationController = GetComponent<PlayerRotationController>();
    }

    private void GetUserId()
    {
        _userId = _collection.UserId;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LastDemoScene")
        {
            if (PlayerModel.activeSelf == false)
            {
                SetPosition();
                PlayerModel.SetActive(true);
            }
            if (isOwned)
            {
                Movement();
                ONCatchPlayer();
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

        _userInput.x = moveX;
        _userInput.z = moveZ;
        _userInput.Normalize();
        _animation.PlayRuning(_userInput.sqrMagnitude);
    }

    public void Jump()
    {

    }

    public void SetPosition()
    {
        _agent.Warp(new Vector3(Random.Range(-5, 5), 0.8f, Random.Range(-15, 7)));
    }

    private void ONCatchPlayer()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _attack.InitAttack();

        }
    }

    public void ONCharacterGetHit()
    {
        if (_characterGotHit) return;
        _collection.ActivateFillBars(false);
        _characterGotHit = true;
        _smashParticle.Play(true);
        _getHitParticle.Play(true);
        _animation.PlayGetHit();
        Invoke(nameof(ResetGetHit), 5f);
    }

    private void ResetGetHit()
    {
        _collection.ActivateFillBars(true);
        _getHitParticle.Stop(true);
        _characterGotHit = false;
    }

    private void ONAllTrashCleaned(int userId)
    {
        _levelEnded = true;
        if (_userId != userId)
        {
            _animation.PlayLost();
            return;
        }
        _animation.PlayWon();

    }

}


