using UnityEngine;
using UnityEngine.InputSystem; // Import the Input System namespace

public class CursorManager : MonoBehaviour
{
    public Transform pivotPoint; // The fixed point around which the object rotates
    public float maxOffsetDistance = 3f; // Maximum distance from the pivot point to the target

    private Camera mainCamera;
    private bool isCursorLocked = true;
    private InputAction rotateAction; // Reference to the Input Action

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
        LockCursor();

        // Initialize the Input Action for controller input
        rotateAction = new InputAction(binding: "<Gamepad>/rightStick"); // Replace with your desired binding
        rotateAction.Enable();
    }

    void FixedUpdate()
    {
        if (isCursorLocked)
        {
            Vector3 directionToMouse;

            if (Gamepad.current != null && rotateAction.ReadValue<Vector2>() != Vector2.zero)
            {
                // Use controller input
                Vector2 inputVector = rotateAction.ReadValue<Vector2>();
                directionToMouse = new Vector3(inputVector.x, inputVector.y, 0f);
            }
            else
            {
                // Use mouse input
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0f));
                directionToMouse = new Vector3(mouseWorldPosition.x,mouseWorldPosition.y,0f) - new Vector3(pivotPoint.position.x, pivotPoint.position.y, 0f);
            }

            // Clamp the offset distance to the maximum value
            float offsetDistance = Mathf.Min(directionToMouse.magnitude, maxOffsetDistance);

            // Calculate the angle between the target's forward vector and the input direction
            float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

            // Calculate the offset position
            Vector3 offsetPosition = pivotPoint.position + directionToMouse.normalized * offsetDistance;

            // Set the position and rotation of the target object
            transform.position = offsetPosition;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void LockCursor()
    {
        Cursor.visible = false; // Hide the cursor
    }

    public void UnlockCursor()
    {
        Cursor.visible = true; // Show the cursor
    }
}
