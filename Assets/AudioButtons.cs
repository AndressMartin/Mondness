using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    public bool disabled;

    public enum Configurator {Music, Sfx};
    public Configurator myConfig;
    private void Start()
    {
        if (myConfig == Configurator.Music) disabled = PointManage.GetInstance().sysConfig.musicMuted;
        else if (myConfig == Configurator.Sfx) disabled = PointManage.GetInstance().sysConfig.sfxMuted;
    }
    public void SetDisabled()
    {
        disabled = !disabled;
        transform.GetChild(1).gameObject.SetActive(disabled);
    }
}
