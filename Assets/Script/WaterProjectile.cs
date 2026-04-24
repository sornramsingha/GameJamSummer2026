using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class  WaterProjectile : MonoBehaviour
{
    [Header("Homing Settings")]
    public Transform target;
    public float speed = 10f;           
    public float homingSensitivity = 5f; 
    public float destroyTime = 4f;     

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) target = playerObj.transform;
        }
        Destroy(gameObject, destroyTime);
    }

    void FixedUpdate()
    {
        if (target == null || rb == null) return;

        Vector3 targetPos = target.position + Vector3.up * 1f;
        Vector3 direction = (targetPos - transform.position).normalized;

        Vector3 newVelocity = Vector3.Lerp(rb.linearVelocity.normalized, direction, homingSensitivity * Time.fixedDeltaTime);
        rb.linearVelocity = newVelocity * speed;
        if (rb.linearVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }
}
