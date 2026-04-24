using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public WinManager winManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winManager != null)
            {
                winManager.WinGame();
            }
        }
    }
}