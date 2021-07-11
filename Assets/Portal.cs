using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private enum PortalType { next, last }
    [SerializeField] PortalType portal = PortalType.next;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerModel")
        {
            if (portal == PortalType.next) TeleportNext();
            else if (portal == PortalType.last) LoadNextScene();
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
