using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Estrela : MonoBehaviour
{
    FMOD.Studio.EventInstance coletaSfx;
    public static UnityEvent coleta;
    private Transform stagePortal;
    private void Start()
    {
        coletaSfx = RuntimeManager.CreateInstance("event:/sfx/coleta_de_estrela");
        stagePortal = transform.parent.Find("Portal");
        CuboManager.ResetarCena.AddListener(ResetParams);
        if (coleta == null)
        {
            coleta = new UnityEvent();
        }
        coleta.AddListener(stagePortal.GetComponent<Portal>().Open);
    }

    private void ResetParams()
    {
        Ativar(true);
        coleta.AddListener(stagePortal.GetComponent<Portal>().Open);
        stagePortal.GetComponent<Portal>().Close();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerModel")
        {
            Debug.Log("COLIDIU COM JOGADOR");
            Coletar();
        }
    }

    private void Coletar()
    {
        RuntimeManager.AttachInstanceToGameObject(coletaSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
        coletaSfx.start();
        //Fazer pozinho de conquista
        //Tocar musiquinha
        coleta.Invoke();
        coleta.RemoveAllListeners();
        Ativar(false);
    }

    void Ativar(bool v)
    {
        transform.GetComponent<BoxCollider>().enabled = v;
        transform.GetChild(0).gameObject.SetActive(v);
    }
}
