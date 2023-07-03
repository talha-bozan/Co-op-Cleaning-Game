using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;


    private NavMeshAgent _agent;
    public Vector3 PlayerInput { get; set; }

    void Start()
    {
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

    private void MoveCharacter()
    {
        _agent.Move(transform.forward * (movementSpeed * Time.deltaTime * PlayerInput.sqrMagnitude));
        //transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime * PlayerInput.sqrMagnitude),Space.Self);
    }
}
