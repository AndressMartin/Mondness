using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static UnityEvent nextScene;

    private void Awake()
    {
        if (nextScene == null)
            nextScene = new UnityEvent();
        nextScene.AddListener(LoadNextScene);
    }
    public void LoadNextScene()
    {
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        //if (nextScene > SceneManager.sceneCount)
        //{
        //    nextScene = 0;
        //}
        SceneManager.LoadScene(nextScene);
    }
}
