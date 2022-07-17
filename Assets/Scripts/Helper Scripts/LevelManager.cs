using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    // Update is called once per frame
    void Update()
    {

    }
    public void ReturnHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("Gameplay");
        GameplayController.instance.gameOver = false;
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        GameplayController.instance.isPausing = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        GameplayController.instance.isPausing = true;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
