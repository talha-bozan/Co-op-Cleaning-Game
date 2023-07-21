using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Transform playerCameraTransform;
    private Vector3 currentRotation;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        playerCameraTransform = _camera.transform;
        currentRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        RotatePlayerWithMouse();
    }

    private void RotatePlayerWithMouse()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            currentRotation.y += mouseX;

            transform.rotation = Quaternion.Euler(currentRotation);

            playerCameraTransform.parent.rotation = Quaternion.Euler(0, currentRotation.y, 0);
        }
    }
}
