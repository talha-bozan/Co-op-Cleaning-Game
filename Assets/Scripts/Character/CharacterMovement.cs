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

    void Start()
    {
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
}
