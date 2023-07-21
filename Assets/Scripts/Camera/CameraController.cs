using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class CameraController : NetworkBehaviour
{
    public Transform playerBody; // Reference to the player's body (rotation controller)
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 5f;
    public GameObject cameraHolder;
    public Vector3 offset;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    public override void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
    }
    private void LateUpdate()
    {
        if (!isOwned || playerBody == null)
            return;

        // Calculate the desired position and rotation based on the player's body
        desiredPosition = playerBody.position + playerBody.TransformDirection(playerBody.position+ offset);
        desiredRotation = Quaternion.Euler(0, playerBody.eulerAngles.y, 0);

        // Smoothly move the camera to the desired position and rotation
        SmoothFollow();
    }
      private void SmoothFollow()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed) ;
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    
    }
}
