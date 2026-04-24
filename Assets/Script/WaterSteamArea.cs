using UnityEngine;

public class WaterStreamArea : MonoBehaviour
{
    [Header("Settings")]
    public float wetnessPerSecond = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWetness pw = other.GetComponent<PlayerWetness>();
            if (pw != null)
            {
                pw.AddWetness(wetnessPerSecond * Time.deltaTime);
            }
        }
    }
}