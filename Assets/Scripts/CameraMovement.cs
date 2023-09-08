using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public Transform player;            // Reference to the player's transform
    public float followRadius = 5.0f;   // The desired radius between camera and player
    public float followSpeed = 5.0f;    // The speed of camera movement

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        transform.position = player.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -20);
        if (player == null)
        {
            Debug.LogWarning("Player reference is missing!");
            return;
        }

        Vector3 playerPosition = player.position;           // Get the player's position
        Vector3 cameraToPlayer = playerPosition - transform.position;

        // Calculate the desired camera position within the follow radius
        Vector3 desiredPosition = playerPosition - cameraToPlayer.normalized * followRadius;

        // Interpolate the camera's position towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
