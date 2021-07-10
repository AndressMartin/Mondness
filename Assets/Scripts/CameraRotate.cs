using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public enum CameraPos { pos1, pos2, pos3, pos4 }
    public CameraPos cameraPos = CameraPos.pos1;
    int posNum = 0;

    //Rotation axis
    float x = 30;
    float y = 45;
    float z = 0;

    //Set
    float rotationSpeed = 45;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            y += 90;
            if (y >= 360) y -= 360;
            posNum++;
            if (posNum > 3) posNum = 0;
            //Debug.Log($"PosNum = {posNum}");
            SetCameraNewPos();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            y -= 90;
            if (y < 0) y += 360;
            posNum--;
            if (posNum < 0) posNum = 3;
            //Debug.Log($"PosNum = {posNum}");
            SetCameraNewPos();
        }
    }

    private void SetCameraNewPos()
    {
        currentEulerAngles = new Vector3(x, y, z);
        //TODO: Fazer IENUMERATOR com animação da câmera
        //Debug.Log($"currentEulerAngles = {currentEulerAngles}");
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
    }
}
