using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint; 
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentCheckpoint = transform.position;
    }
    public void SetCheckpoint(Vector3 newPosition)
    {
        currentCheckpoint = newPosition;
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }

        Debug.Log("Respawned at: " + currentCheckpoint);
    }
}