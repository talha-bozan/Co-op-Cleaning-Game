using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testformovement : MonoBehaviour
{
    public float speed = 5.0f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");  // A/D or Gamepad Left/Right
        float verticalInput = Input.GetAxis("Vertical");    // W/S or Gamepad Up/Down

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            characterController.SimpleMove(direction * speed);
        }
    }
}
