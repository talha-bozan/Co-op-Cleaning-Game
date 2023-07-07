using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float catchRadius = 2.5f;
    [SerializeField]
    private LayerMask playerMask;

    private GameObject caughtPlayer;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool isICaught = false;
    private bool isRunning = false;
    private bool isPunching = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context) { movementInput = context.ReadValue<Vector2>(); }
    public void OnJump(InputAction.CallbackContext context) { jumped = context.action.triggered; }
    public void OnCatch(InputAction.CallbackContext context)
    {
        if (isICaught)
            return; // Do not jump if the player is caught
        if (context.action.triggered)
        {
            catchPlayer();
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.action.triggered;
    }
    public void OnPunch(InputAction.CallbackContext context)
    {
        isPunching = context.action.triggered;
    }
    void Update()
    {   //somehow it should stay here dont change.
        groundedPlayer = controller.isGrounded;

        Movement();
        Jump();
        //TO DO: RUN AND PUNCH NEED TO BE ADDED

    }
    private IEnumerator releasePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // release the player
        caughtPlayer.GetComponent<PlayerControl>().isICaught = false;
        caughtPlayer = null;
    }

    private void Movement()
    {
        if (isICaught)
            return; // Do not move if the player is caught

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
    }
    private void Jump()
    {
        if (isICaught)
            return; // Do not jump if the player is caught

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void catchPlayer()
    {
        if (caughtPlayer != null)
            return; // If we've already caught a player, don't catch another one

        Collider[] playersInRange = Physics.OverlapSphere(transform.position, catchRadius, playerMask);

        foreach (Collider player in playersInRange)
        {
            if (player.gameObject == this.gameObject) // Skip over the "owner" player
                continue;

            caughtPlayer = player.gameObject;

            caughtPlayer.GetComponent<PlayerControl>().isICaught = true;
            // Now we've caught a player, stop checking the rest and start the release timer
            StartCoroutine(releasePlayerAfterDelay(5f));
            return;
        }
    }
    public bool IsPlayerMoving()
    {
        return movementInput != Vector2.zero;
    }
    public bool IsPlayerRunning()
    {
        return isRunning;
    }
    public bool IsPlayerJumping()
    {
        return jumped;
    }
    public bool IsPlayerPunching()
    {
        return isPunching;
    }
}
