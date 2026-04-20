using UnityEngine;

public class WaterObstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public float wetDamage = 20f; 
    public bool destroyOnHit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWetness playerWetness = other.GetComponent<PlayerWetness>();

            if (playerWetness != null)
            {
                playerWetness.AddWetness(wetDamage);

                // (Option) ตรงนี้สามารถเพิ่มโค้ดเล่นเสียงสาดน้ำ หรือ Effect น้ำกระจายได้
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}