using UnityEngine;
using UnityEngine.UI;

public class PlayerWetness : MonoBehaviour
{
    [Header("Wetness Settings")]
    public float maxWetness = 100f;
    public float currentWetness = 0f; 

    [Header("UI References")]
    public Slider wetBar; 

    [Header("Manager References")]
    public GameOverManager gameOverManager; 

    private void Start()
    {
        currentWetness = 0f;
        if (wetBar != null)
        {
            wetBar.maxValue = maxWetness;
            wetBar.value = currentWetness;
        }
    }

    public void AddWetness(float amount)
    {
        currentWetness += amount;
        currentWetness = Mathf.Clamp(currentWetness, 0, maxWetness);

        if (wetBar != null)
        {
            wetBar.value = currentWetness;
        }
        if (currentWetness >= maxWetness)
        {
            Debug.Log("Wetness full! Game Over.");
            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }
}