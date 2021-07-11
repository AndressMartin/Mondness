using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListenerSfx : MonoBehaviour
{
    FMOD.Studio.EventInstance clickSfx;
    FMOD.Studio.EventInstance selectSfx;

    private void Start()
    {
        clickSfx = RuntimeManager.CreateInstance("event:/sfx/interface_click");
        selectSfx = RuntimeManager.CreateInstance("event:/sfx/interface_selecao");
    }
    public void Clique()
    {
        selectSfx.start();
    }
    public void Selecionar()
    {
        clickSfx.start();
    }
}
