using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private enum PortalType { next, last }
    [SerializeField] PortalType portal = PortalType.next;

    [SerializeField] bool open;
    [SerializeField] int starnum;
    private void Start()
    {
        CheckIfOpen();
    }
    public void Open()
    {
        starnum--;
        if (starnum <= 0) open = true;
        CheckIfOpen();
    }
    private void CheckIfOpen()
    {
        if (!open && starnum <= 0)
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
        Debug.Log("Last");
    }
}
