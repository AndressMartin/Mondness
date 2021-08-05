using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManage : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 1)
        {
            Destroy(gameObject);
        }
    }
}
