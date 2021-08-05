using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{


    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    bool musicMuted;
    bool soundMuted;
    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
    }

    private void Start()
    {
        
    }
    void Update()
    {
        if (PointManage.GetInstance().loaded)
        {
            bool musicMuted = PointManage.GetInstance().sysConfig.musicMuted;
            bool soundMuted = PointManage.GetInstance().sysConfig.sfxMuted;
        }
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
    }

    public void MusicMute()
    {
        musicMuted = !musicMuted;
        Music.setMute(musicMuted);
        PointManage.GetInstance().sysConfig.musicMuted = musicMuted;
        PointManage.GetInstance().SaveSys();
    }
    public void SFXMute()
    {
        soundMuted = !soundMuted;
        SFX.setMute(soundMuted);
        PointManage.GetInstance().sysConfig.sfxMuted = soundMuted;
        PointManage.GetInstance().SaveSys();
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