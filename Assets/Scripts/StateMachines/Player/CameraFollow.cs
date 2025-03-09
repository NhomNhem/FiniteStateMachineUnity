using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f;
    public float distance = 5f;
    public float minY = -20f, maxY = 70f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        // Nh?n input chu?t
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // Tính v? trí camera
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 position = player.position - (rotation * Vector3.forward * distance);

        // Gán v? trí và xoay camera
        transform.position = position;
        transform.LookAt(player);
    }
}
