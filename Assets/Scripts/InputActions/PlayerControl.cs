using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    public bool jumpTriggered = false;

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
    [SerializeField]
    private float turnSpeed = 5f; // Adjust this value to your liking. Higher values mean faster rotation.
    [SerializeField]
    private float runMultiplier = 1.5f;

    private GameObject caughtPlayer;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public Vector2 movementInput = Vector2.zero;
    public bool jumped = false;
    public bool isICaught = false;
    public bool isRunning = false;
    public bool isPunching = false;
    public bool isStunned = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
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
        caughtPlayer.GetComponent<PlayerControl>().isStunned = true;
        yield return new WaitForSeconds(delay);
        
        // release the player
        caughtPlayer.GetComponent<PlayerControl>().isICaught = false;
        caughtPlayer.GetComponent<PlayerControl>().isStunned = false;
        caughtPlayer = null;

        
    }

    private void Movement()
    {
        if (isICaught)
            return; // Do not move if the player is caught

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        float speed = playerSpeed;
        if (isRunning)
        {
            speed *= runMultiplier;
        }

        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
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
            jumped = false; // reset jumped after jump execution
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void catchPlayer()
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
        bool wasJumpTriggered = jumpTriggered;
        jumpTriggered = false; // reset the trigger
        return wasJumpTriggered;
    }
    public bool IsPlayerPunching()
    {
        return isPunching;
    }
    public bool IsPlayerStunned()
    {
        return isStunned;
    }
}
