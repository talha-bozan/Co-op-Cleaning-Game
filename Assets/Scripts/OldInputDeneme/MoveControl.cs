using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveControl : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 10.0f;
    private float JumpHeight = 3.0f;
    private float gravityValue = -9.81f;
    private Vector3 move;
    private Vector3 jumpVelocity;
    private bool jumped = false;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 totalMovement = Vector3.zero;

        if (controller.isGrounded)
        {
            if (jumpVelocity.y < 0f)
            {
                jumpVelocity.y = -2f;
            }
        }

        if (!controller.isGrounded)
        {
            jumpVelocity.y += gravityValue * Time.deltaTime;
        }

        totalMovement += jumpVelocity * Time.deltaTime;

        // Move the character forward if there is input
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;


            float speed = Input.GetKey(KeyCode.LeftShift) ? 2 * playerSpeed : playerSpeed;
            totalMovement += move * speed * Time.deltaTime;
        }

        controller.Move(totalMovement);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("context from move is " + context.ToString());
        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {     
        float movement = context.ReadValue<float>();

        

        if (controller.isGrounded)
        {
            jumpVelocity.y = Mathf.Sqrt(-2 * JumpHeight * gravityValue);
        }
    }
}
