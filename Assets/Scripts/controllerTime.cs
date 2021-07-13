using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class controllerTime : MonoBehaviour
{
    //This script was created to make the Cinematica/Story Game transition effect.
    public TextMeshProUGUI CinematicText1 = null;
    public TextMeshProUGUI CinematicText2 = null;
    public TextMeshProUGUI CinematicText3 = null;
    public Button ClickAnywhereButton = null;
    private TextMeshProUGUI clickAnywhereText = null;
    public float fadeTime = 2;
    public float fadeStart = 0;

    public void Start()
    {
        CinematicText1.color = TextFader.SetAlphaZero(CinematicText1.color);
        CinematicText2.color = TextFader.SetAlphaZero(CinematicText2.color);
        CinematicText3.color = TextFader.SetAlphaZero(CinematicText3.color);
        ClickAnywhereButton.interactable = false;
        clickAnywhereText = ClickAnywhereButton.GetComponentInChildren<TextMeshProUGUI>();
        clickAnywhereText.color = TextFader.SetAlphaZero(clickAnywhereText.color);
    }
    public void StartCinematic()
    {
        Debug.Log("Start Cinematic");
        StartCoroutine(WaitBeforeShow());
    }
    private IEnumerator WaitBeforeShow()
    {
        yield return new WaitForSeconds(.5f);
        CinematicText1.gameObject.SetActive(true);
        TextFader.StartFaderToWhite(CinematicText1);
        yield return new WaitForSeconds(2.2f);
        CinematicText2.gameObject.SetActive(true);
        TextFader.StartFaderToWhite(CinematicText2);
        yield return new WaitForSeconds(2.2f);
        CinematicText3.gameObject.SetActive(true);
        TextFader.StartFaderToWhite(CinematicText3);
        yield return new WaitForSeconds(2.2f);
        ClickAnywhereButton.gameObject.SetActive(true);
        ClickAnywhereButton.interactable = true;
        TextFader.StartFaderToWhite(clickAnywhereText);
    }
}


