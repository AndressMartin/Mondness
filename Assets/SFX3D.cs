using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SFX3D : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectsound;
    FMOD.Studio.EventInstance soundEvent;

    public KeyCode pressToPlay;
    private void Start()
    {

        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
    }


    void Playsound()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        soundEvent.start();
    }
}
