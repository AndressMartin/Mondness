using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{


    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    bool musicMuted = false;
    bool soundMuted = false;
    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
    }

    void Update()
    {
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
    }

    public void MusicMute()
    {
        musicMuted = !musicMuted;
        Music.setMute(musicMuted);
    }
    public void SFXMute()
    {
        soundMuted = !soundMuted;
        SFX.setMute(soundMuted);
    }
    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }
}