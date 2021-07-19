using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembroAnim : MonoBehaviour
{
    private PlayerMove playerMove;
    public Animator anim;
    //bool waitToSetIdle;
    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        anim = GetComponent<Animator>();
        PlayerMove.startIdle.AddListener(StartIdle);
        PlayerMove.startRun.AddListener(StartRun);
    }
    public void StartIdle()
    {
        //RUNNING MANY TIMES
        //Debug.LogWarning("IDLE");
        anim.SetBool("Idle", true);
        anim.SetBool("Run", false);
    }

    public void StartRun()
    {
        //RUNNING MANY TIMES
        //Debug.LogWarning("RUN");
        anim.SetBool("Run", true);
        anim.SetBool("Idle", false);
    }

    public void PuloInicio()
    {
        anim.Play("PuloInicio");
    }

    public void Pulo()
    {
        anim.Play("Pulo");
    }

    public void PuloLoop()
    {
        anim.Play("PuloLoop");
    }

    public void PlayerMovePular()
    {
        playerMove.AnimacaoPular();
        Pulo();
    }

    public void AnimacaoIdle()
    {
        anim.Play("Idle");
    }

    public void TerminandoPulo()
    {
        anim.Play("TerminarPulo");
    }
}
