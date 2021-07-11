using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    bool playerEntrou;
    bool caiQuandoPlayerSai = true;
    public bool caindo;
    float velocity = 15f;
    Vector3 quedaDir;
    Vector3 posIni;
    Quaternion rotIni;
    private void Start()
    {
        posIni = transform.position;
        rotIni = transform.rotation;
        CuboManager.ResetarCena.AddListener(ResetParams);
    }

    private void ResetParams()
    {
        transform.position = posIni;
        transform.rotation = rotIni;
        caindo = false;
        playerEntrou = false;
    }

    public virtual void ChecarQueda()
    {
        if (caiQuandoPlayerSai)
        {
            caindo = true;
        }
    }
    public void PegarDir(Vector3 dir)
    {
        quedaDir = dir;
    }
    private void Update()
    {
        if (caindo == true)
        {
            Cair();
        }
    }

    private void Cair()
    {
        transform.position += quedaDir * velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (caindo) 
        {
            Debug.LogWarning("IM COLLINDING");
            if (collision.gameObject.tag == "Bloco")
            {
                Debug.LogWarning("ITS A BLOCK!");
                caindo = false;
                transform.position = collision.transform.position;
                transform.position += quedaDir * -1;
            }
        }
       
    }

}
