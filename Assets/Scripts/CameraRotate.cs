using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    
    public static Enums.CameraPos cameraPos = Enums.CameraPos.pos1;
    int posNum = 0;

    //Rotation axis
    float x = 0;
    float y = 0;
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
            posNum++;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            posNum--;
        }
        else return;
        if (posNum > 3) posNum = 0;
        if (posNum < 0) posNum = 3;
        y = 90 * posNum;
        SetCameraNewPos();
    }

    private void SetCameraNewPos()
    {
        cameraPos = (Enums.CameraPos)posNum;
        currentEulerAngles = new Vector3(x, y, z);
        //TODO: Fazer IENUMERATOR com animação da câmera
        Debug.Log($"currentEulerAngles = {currentEulerAngles}");
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
    }
}

public class Enums 
{
    public enum CameraPos { pos1, pos2, pos3, pos4 }
}

