using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    FMOD.Studio.EventInstance portalSfx;
    FMOD.Studio.EventInstance fanfarraSfx;
    private enum PortalType { next, last }
    [SerializeField] PortalType portal = PortalType.next;

    [SerializeField] bool open;
    [SerializeField] int starNum;
    private int starMax;
    private void Start()
    {
        starMax = starNum;
        portalSfx = RuntimeManager.CreateInstance("event:/sfx/entrando_no_portal");
        CheckIfOpen();
    }
    public void Open()
    {
        starNum--;
        if (starNum <= 0) open = true;
        CheckIfOpen();
    }
    public void Close()
    {
        Debug.Log("CLOSE");
        if (starNum < starMax) starNum++;
        if (starNum > 0) open = false;
        CheckIfOpen();
    }
    private void CheckIfOpen()
    {
        if (!open && starNum <= 0)
        {
            open = true;
        }
        if (open)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (open)
        {
            if (other.gameObject.tag == "PlayerModel")
            {
                RuntimeManager.AttachInstanceToGameObject(portalSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
                portalSfx.start();
                if (portal == PortalType.next) TeleportNext();
                else if (portal == PortalType.last) LoadNextScene();
            }
        }
    }

    private void TeleportNext()
    {
        Debug.Log("Next");
        StageManage.portalTeleport.Invoke();
    }
    private void LoadNextScene()
    {
        //Get level stars. Compare with store. Set the biggest.
        PointManage.GetInstance().SetLevels(SceneManager.GetActiveScene().buildIndex, true, true, starMax - starNum);
        PointManage.GetInstance().SetLevels(SceneManager.GetActiveScene().buildIndex + 1, true, false, 0);
        PointManage.GetInstance().Save();
        Debug.Log("Last");
        SceneManage.nextScene.Invoke();
    }
}
