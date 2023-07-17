using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    public float _speedMultiplier;


    private NavMeshAgent _agent;
    public Vector3 PlayerInput { get; set; }


    //GettingHit
    private CharacterCollection _collection;
    private CharacterAnimation _animation;
    private bool _characterGotHit;
    private ParticleSystem _getHitParticle;
    private ParticleSystem _smashParticle;

    void Start()
    {
        _getHitParticle = transform.GetChild(4).GetComponent<ParticleSystem>();
        _smashParticle = transform.GetChild(5).GetComponent<ParticleSystem>();
        _animation = GetComponent<CharacterAnimation>();
        _collection = GetComponent<CharacterCollection>();
        _speedMultiplier = 1f;
        _agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        UpdateRotation();
        MoveCharacter();
    }

    private void UpdateRotation()
    {
        if (PlayerInput.sqrMagnitude <= .1f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerInput), Time.deltaTime * rotationSpeed);
    }

    public void OnCharacterRun(bool isPressing)
    {
        if (_characterGotHit) return;
        if (isPressing)
        {
            _speedMultiplier = 1.5f;
        }
        else
        {
            _speedMultiplier = 1f;
        }
    }

    private void MoveCharacter()
    {
        _agent.Move(transform.forward * (movementSpeed*_speedMultiplier * Time.deltaTime * PlayerInput.sqrMagnitude));
        //transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime * PlayerInput.sqrMagnitude),Space.Self);
    }


    public void ONCharacterGetHit()
    {
        if (_characterGotHit) return;
        _collection.ActivateFillBars(false);
        _characterGotHit = true;
        _smashParticle.Play(true);
        _getHitParticle.Play(true);
        _speedMultiplier = .1f;
        _animation.PlayGetHit();
        Invoke(nameof(ResetGetHit), 3f);
    }

    private void ResetGetHit()
    {
        _collection.ActivateFillBars(true);
        _getHitParticle.Stop(true);
        _characterGotHit = false;
        _speedMultiplier = 1f;
    }
}
