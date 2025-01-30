using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;         // Reference to the player
    public Vector3 offset;          // The offset from the player
    public float smoothSpeed = 0.125f; // Smoothness factor for the camera movement

    void LateUpdate()
    {
        // Calculate the desired position based on player's position and the offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;

        // Optionally, you can make the camera always look at the player
        // transform.LookAt(player);
    }
}
