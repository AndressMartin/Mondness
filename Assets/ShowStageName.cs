using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowStageName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] Image blur;
    bool isStageTextShown = false;

    float fadeTimeBlur = 2;
    float fadeStartBlur = 0;
    Color colorGoal;

    // Start is called before the first frame update
    void Start()
    {
        blur = transform.GetChild(0).GetComponent<Image>();
        stageText = blur.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        StageManage.puzzlesLoaded.AddListener(ShowUI);
        stageText.color = TextFader.SetAlphaZero(stageText.color);
    }

    private void ShowUI()
    {
        StartCoroutine(StageHeaderAnimations());
    }

    private IEnumerator StageHeaderAnimations()
    {
        yield return new WaitForSeconds(.5f); 
        TextFader.StartFaderToWhite(stageText);
        yield return new WaitForSeconds(2.2f);
        TextFader.StartFaderToBlack(stageText);
        if (isStageTextShown != true)
        {
            isStageTextShown = true;
            colorGoal = new Color(blur.color.r, blur.color.g, blur.color.b, 0);
        }
        yield return new WaitForSeconds(3f);
        //blur.gameObject.SetActive(false);
    }

    private void FadeOutBlur()
    {
        if (fadeStartBlur < fadeTimeBlur)
        {
            fadeStartBlur += Time.deltaTime;
            blur.color = Color.Lerp(blur.color, colorGoal, 3* Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStageTextShown)
        {
            FadeOutBlur();
        }
    }
}
