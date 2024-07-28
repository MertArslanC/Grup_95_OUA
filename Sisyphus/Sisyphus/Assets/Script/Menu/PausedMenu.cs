using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public static bool oyunudurdumu = false;
    public GameObject canvasPause;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (oyunudurdumu == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        canvasPause.SetActive(false);
        Time.timeScale = 1f;
        oyunudurdumu = false;
    }

    public void Pause()
    {
        canvasPause.SetActive(true);
        Time.timeScale = 0f;
        oyunudurdumu = true;

    }
    public void Restart()
    {
        Time.timeScale = 1f; 
        oyunudurdumu = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
