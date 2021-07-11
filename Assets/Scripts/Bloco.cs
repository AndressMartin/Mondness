using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    bool playerEntrou;
    bool caiQuandoPlayerSai = true;
    public bool caindo,
                flutuando;
    float velocity = 15f;
    Vector3 quedaDir;
    Vector3 posIni;
    Quaternion rotIni;

    float tempoCaindo = 0;
    float maxTempoCaindo = 0.9f;
    Vector3 velF;
    private void Start()
    {
        posIni = transform.position;
        rotIni = transform.rotation;
        CuboManager.ResetarCena.AddListener(ResetParams);

        velF = new Vector3(0, 0, 0);
    }

    private void ResetParams()
    {
        transform.position = posIni;
        transform.rotation = rotIni;
        caindo = false;
        flutuando = false;
        playerEntrou = false;
    }

    public virtual void ChecarQueda()
    {
        if (caiQuandoPlayerSai)
        {
            caindo = true;
            tempoCaindo = 0;
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
            if(tempoCaindo > maxTempoCaindo)
            {
                caindo = false;
                Flutuar();
            }
        }

        if (flutuando == true)
        {
            transform.Rotate(velF * Time.deltaTime);
        }
    }

    private void Cair()
    {
        transform.position += quedaDir * velocity * Time.deltaTime;
        tempoCaindo += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (caindo) 
        {
            Debug.LogWarning("IM COLLINDING");
            if (collision.gameObject.tag == "Bloco")
            {
                Debug.LogWarning("ITS A BLOCK!");
                if (collision.gameObject.GetComponent<Bloco>().flutuando == false)
                {
                    caindo = false;
                    transform.position = collision.transform.position;
                    transform.position += quedaDir * -1;
                }
                else
                {
                    caindo = false;
                    Flutuar();
                }
            }
        }
    }

    private void Flutuar()
    {
        velF = new Vector3(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f));
        flutuando = true;
    }

}
