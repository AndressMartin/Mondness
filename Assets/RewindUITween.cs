using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindUITween : MonoBehaviour
{
    public RectTransform button;
    [SerializeField] Vector3 PosIni;
    public RectTransform point;
    [SerializeField] Vector3 pointPos;
    void Start()
    {
        PosIni = button.localPosition;
        pointPos = point.localPosition;
        //PlayerMove.flutuando.AddListener(ShowButton);
        //CuboManager.ResetarCena.AddListener(MoveBack);
        LeanTween.move(button, pointPos, 2f).setDelay(6f).setEase(LeanTweenType.easeOutCirc);
    }

    //void ShowButton()
    //{
    //    LeanTween.move(button, pointPos, 2f).setDelay(2f).setEase(LeanTweenType.easeOutCirc);
    //}

    //void MoveBack()
    //{
    //    LeanTween.move(button, PosIni, 2f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc);
    //}
}
