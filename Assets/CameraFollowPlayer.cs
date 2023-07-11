using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform playerHead; // Reference to the player's head transform

    void LateUpdate()
    {
        // Match the camera's position and rotation exactly to the player's head
        transform.position = playerHead.position;
        transform.rotation = playerHead.rotation;
    }
}
