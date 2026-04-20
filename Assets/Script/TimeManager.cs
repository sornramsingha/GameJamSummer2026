using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float startHour = 8f;
    public float endHour = 12f;
    public float timeMultiplier = 60f;

    private float currentTimeInSeconds;
    private float endTimeInSeconds;
    private bool timerIsRunning = false;

    [Header("UI References")]
    public TextMeshProUGUI clockText;

    [Header("Manager References")]
    public GameOverManager gameOverManager;

    private void Start()
    {
        currentTimeInSeconds = startHour * 3600f;
        endTimeInSeconds = endHour * 3600f;

        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (currentTimeInSeconds < endTimeInSeconds)
            {
                currentTimeInSeconds += Time.deltaTime * timeMultiplier;
                DisplayTime(currentTimeInSeconds);
            }
            else
            {
                currentTimeInSeconds = endTimeInSeconds;
                DisplayTime(currentTimeInSeconds);
                timerIsRunning = false;

                if (gameOverManager != null)
                {
                    gameOverManager.ShowGameOver();
                }
            }
        }
    }

    void DisplayTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        string dayPart = hours >= 12 ? "PM" : "AM";

        int displayHours = hours;
        if (hours > 12) displayHours -= 12;
        if (hours == 0) displayHours = 12;
        clockText.text = string.Format("{0:00}:{1:00}:{2:00} {3}", displayHours, minutes, seconds, dayPart);
    }
}