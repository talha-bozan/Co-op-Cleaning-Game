using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private PlayerControl playerMovement;
    private Animator animator;
    private static readonly int isWalkingAnimation = Animator.StringToHash("isWalking");
    private static readonly int isRunningAnimation = Animator.StringToHash("isRunning");
    private static readonly int isJumpingAnimation = Animator.StringToHash("isJumping");
    private static readonly int isPunchingAnimation = Animator.StringToHash("isPunching");
    private static readonly int isStunningAnimation = Animator.StringToHash("isStunned");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
        Running();
        Jumping();
        Punching();
        Stunning();
    }

    private void Movement() {
        bool isMoving = playerMovement.IsPlayerMoving();
        animator.SetBool(isWalkingAnimation, isMoving);
    }
    private void Running()
    {
        bool isRunning = playerMovement.IsPlayerRunning();
        animator.SetBool(isRunningAnimation, isRunning);
    }
    private void Jumping()
    {
        if (playerMovement.IsPlayerJumping())
        {
            animator.SetTrigger(isJumpingAnimation);
        }
    }
    private void Punching()
    {
        bool isPunching = playerMovement.IsPlayerPunching();
        animator.SetBool(isPunchingAnimation, isPunching);
    }
    private void Stunning()
    {
        bool isStunning = playerMovement.IsPlayerStunned();
        animator.SetBool(isStunningAnimation, isStunning);
    }
}
