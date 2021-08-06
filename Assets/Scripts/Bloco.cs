using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    [SerializeField] public TipoBloco tipo;


    BoxCollider boxCollider;
    Rigidbody rigidBody;
    MeshRenderer meshRenderer;

    FMOD.Studio.EventInstance quebrarSfx;

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

        quebrarSfx = RuntimeManager.CreateInstance("event:/sfx/bloco_vidro_quebrando");

        velF = new Vector3(0, 0, 0);

        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public enum TipoBloco
    {
        Normal,
        Gelo,
        Vidro,
        Nuvem
    }

    private void ResetParams()
    {
        transform.position = posIni;
        transform.rotation = rotIni;
        caindo = false;
        flutuando = false;

        boxCollider.enabled = true;
        rigidBody.isKinematic = false;
        meshRenderer.enabled = true;
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
            //Debug.LogWarning("IM COLLINDING");
            if (collision.gameObject.tag == "Bloco")
            {
                var bloco = collision.gameObject.GetComponent<Bloco>();
                //Debug.LogWarning("ITS A BLOCK!");
                if (bloco.flutuando == false)
                {
                    if(bloco.tipo == TipoBloco.Normal || bloco.tipo == TipoBloco.Gelo)
                    {
                        caindo = false;
                        transform.position = collision.transform.position;
                        transform.position += quedaDir * -1;
                    }
                    else if(bloco.tipo == TipoBloco.Nuvem)
                    {
                        caindo = false;
                        transform.position = collision.transform.position;
                        bloco.Desativar();
                    }
                    else if(bloco.tipo == TipoBloco.Vidro)
                    {
                        bloco.Quebrar();
                    }
                  
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

    private void Quebrar()
    {
        quebrarSfx.start();
        boxCollider.enabled = false;
        rigidBody.isKinematic = true;
        meshRenderer.enabled = false;
    }

    private void Desativar()
    {
        boxCollider.enabled = false;
        rigidBody.isKinematic = true;
        meshRenderer.enabled = false;
    }

}
