using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuboManager : MonoBehaviour
{
    public static UnityEvent ResetarCena;
    void Awake()
    {
        if (ResetarCena == null)
        {
            ResetarCena = new UnityEvent();
        }
    }

    private void Update()
    {
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
