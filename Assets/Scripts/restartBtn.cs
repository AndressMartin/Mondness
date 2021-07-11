using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class restartBtn : MonoBehaviour
{
    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("GameTest")
    }
}
