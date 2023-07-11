using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerWithCamera : MonoBehaviour
{
    public float sensitivity = 2f; // Mouse sensitivity for character rotation
    public float zoomSpeed = 5f; // Speed of zooming in
    public float zoomAmount = 1f; // Amount of zoom

    private float rotationY = 0f; // Current rotation around the Y-axis
    private float defaultFieldOfView; // Default field of view of the camera

    private void Start()
    {
        // Store the default field of view of the camera
        defaultFieldOfView = Camera.main.fieldOfView;
    }

    private void Update()
    {
        // Rotate the character based on mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseX;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Limit horizontal rotation

        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f); // Apply horizontal rotation to the character
        Camera.main.transform.localRotation = Quaternion.Euler(-mouseY, rotationY, 0f); // Apply vertical rotation to the camera

        Debug.Log("Mouse Y: " + mouseY); // Print the mouse Y-axis movement to the console

        // Zoom in when Mouse 1 is pressed
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView -= zoomSpeed * zoomAmount * Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 1f, 100f); // Limit the field of view
        }
        else
        {
            // Reset the field of view to the default when Mouse 1 is released
            Camera.main.fieldOfView = defaultFieldOfView;
        }
    }
}
