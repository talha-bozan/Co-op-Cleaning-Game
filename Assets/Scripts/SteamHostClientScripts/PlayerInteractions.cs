using System.Collections;
using UnityEngine;
using Mirror;

public class PlayerInteractions : NetworkBehaviour
{
    [SerializeField] private float interactionRange = 5f;
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private float jumpForce = 5f;
    [SyncVar(hook = nameof(OnHealthChanged))] private int health = 100;

    private void Update()
    {
        if (!isLocalPlayer || !Input.GetKeyDown(KeyCode.LeftControl))
            return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactionMask);
        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent<PlayerInteractions>(out PlayerInteractions targetInteractions) && targetInteractions != this)
            {
                // If we hit another player/enemy, perform the interaction
                targetInteractions.TakeDamage(10);
                targetInteractions.Jump();
                CmdPerformInteraction(targetInteractions.gameObject);
            }
        }
    }

    [Command]
    private void CmdPerformInteraction(GameObject target)
    {
        if (target.TryGetComponent<PlayerInteractions>(out PlayerInteractions targetInteractions))
        {
            targetInteractions.TakeDamage(10);
            targetInteractions.Jump();
        }
    }

    private void TakeDamage(int damageAmount)
    {
        if (!isServer)
            return;

        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            // Handle player/enemy death here if you want
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            GetComponent<CharacterController>().Move(Vector3.up * jumpForce);
        }
    }

    private void OnHealthChanged(int oldHealth, int newHealth)
    {
        // You can update the health UI or perform other actions when health changes.
        Debug.Log("Health changed: " + newHealth);
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
}
