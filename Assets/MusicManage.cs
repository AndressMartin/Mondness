using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD;


public class MusicManage : MonoBehaviour
{
    FMOD.Studio.EventInstance gameplayMusic;
    void Start()
    {
        UnityEngine.Debug.Log("Start called");
        gameplayMusic = RuntimeManager.CreateInstance("event:/music/gameplay");
        gameplayMusic.start();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 1)
        {
            gameplayMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Destroy(gameObject);
        }
    }
}
