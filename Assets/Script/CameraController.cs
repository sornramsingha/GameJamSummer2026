using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 8f, -6f); 
    public float smoothSpeed = 5f; 

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}