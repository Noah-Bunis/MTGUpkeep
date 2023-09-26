using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public float rotationSpeed = 5.0f; // Rotation speed around the z-axis
    public Transform pivotPoint; // The fixed point around which the object rotates
    public float offsetDistance = 2.0f; // Distance from the pivot point to the target

    private Camera mainCamera;
    private bool isCursorLocked = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
        LockCursor();
    }

    void Update()
    {
        if (isCursorLocked)
        {
            // Get the mouse position in screen space
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Convert the screen position to a world position
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, transform.position.z));

            // Calculate the angle between the target's forward vector and the vector to the mouse
            Vector3 directionToMouse = mouseWorldPosition - pivotPoint.position;
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
