using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Settings")]
    public Color activeColor = Color.green;
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

            if (playerRespawn != null)
            {
                playerRespawn.SetCheckpoint(transform.position);
                isActivated = true;
                Debug.Log("Checkpoint Activated!");

                // (Option) ตรงนี้สามารถใส่ Effect แสงสี หรือเสียงตอนเซฟได้
            }
        }
    }
}