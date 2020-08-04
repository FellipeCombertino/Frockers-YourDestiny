using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    bool gameIsPaused;
    public GameObject pauseMenuPanel;

    public AudioSource mainSound, birdSound;

    void Update()
    {
        TriggerPause();
    }

    void TriggerPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        if (mainSound)
        {
            mainSound.Pause();
        }
        if (birdSound)
        {
            birdSound.Pause();
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        if (mainSound)
        {
            mainSound.UnPause();
        }
        if (birdSound)
        {
            birdSound.UnPause();
        }
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
