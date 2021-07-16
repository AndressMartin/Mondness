using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFader : MonoBehaviour
{
    public static bool canFade;
    float fadeTime = 2;
    float fadeStart = 0;
    public static TextMeshProUGUI obj;
    public static Color textColor;
    public static Color ColorGoal;
    public static bool canFlash;

    private void Update()
    {
        if (canFade) FadeAlpha();
        if (canFlash) StartCoroutine(FlashText());
    }
    public static void StartFaderToWhite(TextMeshProUGUI Externalobj)
    {
        SetFade(Externalobj);
        ColorGoal = new Color(obj.color.r, obj.color.g, obj.color.b, 1);
    }

    public static void StartFaderToBlack(TextMeshProUGUI Externalobj)
    {
        SetFade(Externalobj);
        ColorGoal = new Color(obj.color.r, obj.color.g, obj.color.b, 0);
    }

    public static void StartFlash(TextMeshProUGUI Externalobj)
    {
        canFlash = true;
    }
    public static Color SetAlphaZero(Color c)
    {
        return new Color(c.r, c.g, c.b, 0);
    }
    private static void SetFade(TextMeshProUGUI Externalobj)
    {
        canFade = true;
        obj = Externalobj;
        textColor = obj.color;
    }
    private void FadeAlpha()
    {
        if (fadeStart < fadeTime)
        {
            fadeStart += Time.deltaTime * fadeTime;
            obj.color = Color.Lerp(textColor, ColorGoal, fadeStart);
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        canFade = false;
        fadeStart = 0;
    }

    public IEnumerator FlashText()
    {
        while (canFlash)
        {
            obj.enabled = false;
            yield return new WaitForSeconds(.5f);
            obj.enabled = true;
            yield return new WaitForSeconds(.5f);
        }
    }
}
