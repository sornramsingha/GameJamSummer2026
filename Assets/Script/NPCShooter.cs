using UnityEngine;

public class NPCShooter : MonoBehaviour
{
    [Header("Detection Settings")]
    public Transform player;
    public float detectionRadius = 8f;

    [Header("Shooting Settings")]
    public GameObject waterPrefab;
    public Transform firePoint;
    public float fireForce = 15f;
    public float fireRate = 1.5f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer < 5f && Time.time >= nextFireTime)
            {
                ShootWater();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ShootWater()
    {
        if (waterPrefab == null || firePoint == null) return;

        GameObject waterObj = Instantiate(waterPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = waterObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 targetAim = player.position + Vector3.up * 1f;
            Vector3 exactDirection = (targetAim - firePoint.position).normalized;
            rb.AddForce(exactDirection * fireForce, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}