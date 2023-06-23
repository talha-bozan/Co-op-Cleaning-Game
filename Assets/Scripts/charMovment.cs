using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charMovment : MonoBehaviour
{
    [SerializeField] private Transform _characterModel;
    private bool _isCharacterActive = true;
    private bool _canMove = true;

    private float turnSmoothTime;
    private float turnSmoothVelocity;

    public CharacterController controller;

    private void Start()
    {
        _canMove = true;
    }

    void Update()
    {
        if (_isCharacterActive == false && _canMove)
        {
            MovementWithoutChar(5f);
        }
        else if (_isCharacterActive && _canMove)
        {
            MovementWithChar();
        }
    }

    public void SetMovementEnabled(bool canMove)
    {
        _canMove = canMove;
    }

    private void MovementWithoutChar(float movementSpeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * (Time.deltaTime * movementSpeed));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.left * (Time.deltaTime * movementSpeed));
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * (Time.deltaTime * movementSpeed));
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * (Time.deltaTime * movementSpeed));
        }

    }
    private void MovementWithChar()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * 5f * Time.deltaTime);
        }

    }
}
