using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuboManager : MonoBehaviour
{
    public static UnityEvent ResetarCena;
    private bool gameStarted;
    void Awake()
    {
        if (ResetarCena == null)
        {
            ResetarCena = new UnityEvent();
        }
    }
    private void Start()
    {
        StageManage.puzzlesLoaded.AddListener(comecar);
    }

    private void comecar()
    {
        gameStarted = true;
    }

    private void Update()
    {
        if (gameStarted != true) return;
        if (Input.GetKeyDown(KeyCode.C))
        {
            SendRespawnEvent();
        }
    }
    void SendRespawnEvent()
    {
        ResetarCena.Invoke();
    }
}
