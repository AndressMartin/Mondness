using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    FMOD.Studio.EventInstance rotateSfx;

    public static Enums.CameraPos cameraPos = Enums.CameraPos.pos1;
    int posNum = 0;

    public static bool rotacionando = false,
                       jogadorParado = false;

    float rotVel = 90f,
          direcao = 0f;

    float maxRotTime = 0.6f;
    float rotTime;

    //Rotation axis
    /*
    float x = 0;
    float y = 0;
    float z = 0;
    */

    //Set
    /*
    float rotationSpeed = 45;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    */

    // Start is called before the first frame update
    void Start()
    {
        rotateSfx = RuntimeManager.CreateInstance("event:/sfx/rotacao_de_camera");

        rotVel = rotVel / maxRotTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) SoundTester.PlaySound();
        //Debug.Log("Came Pos: " + posNum + "\nAngulo: " + 90 * posNum);

        if (rotacionando == false)
        {
            if (jogadorParado == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    RuntimeManager.AttachInstanceToGameObject(rotateSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
                    rotateSfx.start();
                    //transform.Rotate(0, 90, 0);
                    posNum++;

                    direcao = 1f;
                    rotacionando = true;
                    rotTime = maxRotTime;
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    RuntimeManager.AttachInstanceToGameObject(rotateSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
                    rotateSfx.start();
                    //transform.Rotate(0, -90, 0);
                    posNum--;

                    direcao = -1f;
                    rotacionando = true;
                    rotTime = maxRotTime;
                }
                else return;
            }
            else return;
        }
        else
        {
            if(rotTime > 0)
            {
                Rotacionar();

                if(rotTime <= 0)
                {
                    CorrectRotation();
                    rotTime = 0;
                    rotacionando = false;
                }
            }

            return;
        }

        if (posNum > 3) posNum = 0;
        if (posNum < 0) posNum = 3;
        cameraPos = (Enums.CameraPos)posNum;
        //y = 90 * posNum;
        //SetCameraNewPos();
    }

    /*
    private void SetCameraNewPos()
    {
        cameraPos = (Enums.CameraPos)posNum;
        currentEulerAngles = new Vector3(transform.rotation.x, y, transform.rotation.z);
        //TODO: Fazer IENUMERATOR com animação da câmera
        Debug.Log($"currentEulerAngles = {currentEulerAngles}");
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
    }
    */

    private void Rotacionar()
    {
        rotTime -= Time.deltaTime;
        transform.Rotate(0, rotVel * direcao * Time.deltaTime, 0);
    }

    private void CorrectRotation()
    {
        var vec = transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        transform.eulerAngles = vec;
    }

    public static void AtualizarJogadorParado(bool jogadorParado)
    {
        CameraRotate.jogadorParado = jogadorParado;
    }
}

public class SoundTester
{
    public static int i = 0;
    public static List<FMOD.Studio.EventInstance> sounds = new List<FMOD.Studio.EventInstance>()
    {
        RuntimeManager.CreateInstance("event:/sfx/bloquinho_caindo"),
        RuntimeManager.CreateInstance("event:/sfx/coleta_de_estrela"),
        RuntimeManager.CreateInstance("event:/sfx/entrando_no_portal"),
        RuntimeManager.CreateInstance("event:/sfx/fanfarra_vitoria"),
        RuntimeManager.CreateInstance("event:/sfx/grito_do_macaco"),
        RuntimeManager.CreateInstance("event:/sfx/interface_click"),
        RuntimeManager.CreateInstance("event:/sfx/interface_selecao"),
        RuntimeManager.CreateInstance("event:/sfx/morrendo"),
        RuntimeManager.CreateInstance("event:/sfx/passos"),
        RuntimeManager.CreateInstance("event:/sfx/pop_up_de_texto_ui"),
        RuntimeManager.CreateInstance("event:/sfx/rotacao_de_camera"),
        RuntimeManager.CreateInstance("event:/sfx/salto_pulo"),
        RuntimeManager.CreateInstance("event:/sfx/salto_queda"),
    };

    public static void PlaySound()
    {
        sounds[i].start();
        i++;
        Debug.Log(i.ToString());
        if (i >= sounds.Count) i = 0;
    }
}

public class Enums 
{
    public enum CameraPos { pos1, pos2, pos3, pos4 }
}

