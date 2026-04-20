using UnityEngine;

public class WaterSplashDebuff : MonoBehaviour
{
    public float slowMultiplier = 0.4f; 
    public float slowDuration = 3f;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.ApplyTemporarySlow(slowMultiplier, slowDuration);
            }
            // Destroy(gameObject); 
        }
    }
}