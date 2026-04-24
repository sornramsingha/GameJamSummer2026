using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject winMenuUI;

    private void Start()
    {
        if (winMenuUI != null)
        {
            winMenuUI.SetActive(false);
        }
    }

    public void WinGame()
    {
        if (winMenuUI != null)
        {
            winMenuUI.SetActive(true); 
        }

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Mission Accomplished!");
    }

    //public void NextLevel()
    //{
        //Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }
}