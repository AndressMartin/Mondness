using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //timeScale 1.0 = as fast as realtime, timeScale 0.5 = 2x slower than realtime and timeScale 0 = game is paused
    }

    //Back to Game
    public void BackToGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //Back to Home
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
    
}
