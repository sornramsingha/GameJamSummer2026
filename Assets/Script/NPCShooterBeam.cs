using UnityEngine;

public class NPCShooterBeam : MonoBehaviour
{
    [Header("Detection")]
    public Transform player;
    public float detectionRadius = 15f;
    public float rotationSpeed = 5f;

    [Header("Water Stream")]
    public GameObject waterStreamObject;
    public ParticleSystem waterParticle;

    [Header("Burst Settings")]
    public float shootDuration = 3f;   
    public float restDuration = 2f;    

    private float timer;               
    private bool isShooting = false;
    private bool isResting = false;    

    void Start()
    {
        if (waterStreamObject != null)
        {
            waterStreamObject.SetActive(false);
            if (waterParticle == null)
                waterParticle = waterStreamObject.GetComponentInChildren<ParticleSystem>();
        }

        timer = shootDuration;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            RotateTowardsPlayer();
            HandleShootingCycle();
        }
        else
        {
            if (isShooting) StopShooting();
            isResting = false;
            timer = shootDuration;
        }
    }

    void HandleShootingCycle()
    {
        timer -= Time.deltaTime;

        if (!isResting) 
        {
            if (!isShooting) StartShooting();

            if (timer <= 0) 
            {
                StopShooting();
                isResting = true;
                timer = restDuration; 
            }
        }
        else 
        {
            if (timer <= 0)
            {
                isResting = false;
                timer = shootDuration;
            }
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 targetPosition = player.position + Vector3.up * 1f;
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void StartShooting()
    {
        isShooting = true;
        if (waterStreamObject != null) waterStreamObject.SetActive(true);
        if (waterParticle != null) waterParticle.Play();
    }

    void StopShooting()
    {
        isShooting = false;
        if (waterStreamObject != null) waterStreamObject.SetActive(false);
        if (waterParticle != null) waterParticle.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}