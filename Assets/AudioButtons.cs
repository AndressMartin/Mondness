using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    public bool disabled = false;

    public void SetDisabled()
    {
        disabled = !disabled;
        transform.GetChild(1).gameObject.SetActive(disabled);
    }
}
