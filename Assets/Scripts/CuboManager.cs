using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuboManager : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
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
        if (InputExt.GetKeyDown(KeyCode.C))
        {
            SendRespawnEvent();
        }
    }
    public void SendRespawnEvent()
    {
        if (playerMove.estado != PlayerMove.State.Teleportando)
        {
            Debug.Log("REQUEST TO RESPAWN");
            ResetarCena.Invoke();
        }
    }
}
