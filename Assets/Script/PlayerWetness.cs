using UnityEngine;
using UnityEngine.UI;

public class PlayerWetness : MonoBehaviour
{
    [Header("Wetness Settings")]
    public float maxWetness = 100f;
    public float currentWetness = 0f;

    [Header("UI References")]
    public Slider wetBar;
    public GameOverManager gameOverManager;

    [Header("Portrait Settings")]
    public Image portraitImage;        
    public Sprite normalPortrait;      
    public Sprite wetPortrait;          
    public float wetThreshold = 75f;    

    private void Start()
    {
        currentWetness = 0f;
        if (wetBar != null)
        {
            wetBar.maxValue = maxWetness;
            wetBar.value = currentWetness;
        }


        if (portraitImage != null && normalPortrait != null)
        {
            portraitImage.sprite = normalPortrait;
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

        if (portraitImage != null)
        {
            if (currentWetness >= wetThreshold && wetPortrait != null)
            {
                portraitImage.sprite = wetPortrait;
            }
            else if (currentWetness < wetThreshold && normalPortrait != null)
            {
                portraitImage.sprite = normalPortrait;
            }
        }

        if (currentWetness >= maxWetness)
        {
            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }
}