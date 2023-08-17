using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform player;            // Reference to the player's transform
    public float followRadius = 5.0f;   // The desired radius between camera and player
    public float followOffsetX = 0.0f;
    public float followOffsetY = 0.0f;
    public float followSpeed = 5.0f;    // The speed of camera movement
    public bool isCameraPanning = false;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (player == null)
        {
            Debug.LogWarning("Player reference is missing!");
            return;
        }

        Vector3 playerPosition = player.position;           // Get the player's position
        Vector3 cameraToPlayer = playerPosition - transform.position;

        // Calculate the desired camera position within the follow radius
        Vector3 desiredPosition = playerPosition - cameraToPlayer.normalized * followRadius + new Vector3(followOffsetX, followOffsetY, 0.0f);

        CheckCameraPanning(cameraToPlayer, desiredPosition);
        if (isCameraPanning) PanCamera(cameraToPlayer, desiredPosition);
    }

    private void CheckCameraPanning(Vector3 cameraToPlayer, Vector3 desiredPosition)
    {
        if (Mathf.Abs(cameraToPlayer.x) > followRadius || Mathf.Abs(cameraToPlayer.y) > followRadius)
        {
            isCameraPanning = true;
        }
    }

    private void PanCamera(Vector3 cameraToPlayer, Vector3 desiredPosition)
    {
        // Interpolate the camera's position towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        if (Mathf.Abs(cameraToPlayer.x) < 1f && Mathf.Abs(cameraToPlayer.y) < 1f) 
        {
            isCameraPanning = false;
        }
    }
}
