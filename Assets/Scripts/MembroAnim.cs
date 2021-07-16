using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembroAnim : MonoBehaviour
{
    Animator anim;
    //bool waitToSetIdle;
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerMove.startIdle.AddListener(StartIdle);
        PlayerMove.startRun.AddListener(StartRun);
    }
    private void StartIdle()
    {
        //RUNNING MANY TIMES
        Debug.LogWarning("IDLE");
        anim.SetBool("Idle", true);
        anim.SetBool("Run", false);
    }

    private void StartRun()
    {
        //RUNNING MANY TIMES
        Debug.LogWarning("RUN");
        anim.SetBool("Run", true);
        anim.SetBool("Idle", false);
    }
}
