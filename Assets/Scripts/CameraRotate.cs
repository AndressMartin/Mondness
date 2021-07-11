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

    //Rotation axis
    float x = 0;
    float y = 0;
    float z = 0;

    //Set
    float rotationSpeed = 45;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        rotateSfx = RuntimeManager.CreateInstance("event:/sfx/rotacao_de_camera");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) SoundTester.PlaySound();
        //Debug.Log("Came Pos: " + posNum + "\nAngulo: " + 90 * posNum);
        if (Input.GetKeyDown(KeyCode.E))
        {
            rotateSfx.start();
            transform.Rotate(0, 90, 0);
            posNum++;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            rotateSfx.start();
            transform.Rotate(0, -90, 0);
            posNum--;
        }
        else return;
        if (posNum > 3) posNum = 0;
        if (posNum < 0) posNum = 3;
        y = 90 * posNum;
        cameraPos = (Enums.CameraPos)posNum;
        //SetCameraNewPos();
    }

    private void SetCameraNewPos()
    {
        cameraPos = (Enums.CameraPos)posNum;
        currentEulerAngles = new Vector3(transform.rotation.x, y, transform.rotation.z);
        //TODO: Fazer IENUMERATOR com animação da câmera
        Debug.Log($"currentEulerAngles = {currentEulerAngles}");
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
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

