using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  
    public Transform player;     // Drag your player object here
    public Vector3 offset = new Vector3(0, 5, -7);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        // Target position = player position + offset
        Vector3 targetPosition = player.position + offset;

        // Smoothly move camera towards the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Make camera look at player (optional)
        transform.LookAt(player);
    }
}
