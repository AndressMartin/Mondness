using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]GameObject mainCamParent;
    Vector3 cameraV3;
    float horizontal;
    float vertical;
    float velocidade;

    bool caindo;

    public enum FaceEnum { Cima, Baixo, Esquerda, Direita, Frente, Costas}
    public enum CameraPos { pos1, pos2, pos3, pos4}
    public enum Direction { W, D, S, A}
    public FaceEnum faceAtual = FaceEnum.Cima;
    public FaceEnum proximaFace = FaceEnum.Frente;
    public CameraPos cameraPos = CameraPos.pos1;
    public Direction direcao = Direction.W;
    private void Awake()
    {
        cameraV3 = new Vector3(0, 45, 0);
    }
    public void ChecarCamera()
    {
        Debug.Log(mainCamParent.transform.rotation);
        //Se a câmera está no ponto inicial
        if (mainCamParent.transform.rotation == Quaternion.Euler(mainCamParent.transform.rotation.x, 45, mainCamParent.transform.rotation.z))
        {
            Debug.Log(45);
            cameraPos = CameraPos.pos1;
        }
        else if (mainCamParent.transform.rotation == Quaternion.Euler(mainCamParent.transform.rotation.x, 135, mainCamParent.transform.rotation.z))
        {
            Debug.Log(135);
            cameraPos = CameraPos.pos2;
        }
        else if (mainCamParent.transform.rotation == Quaternion.Euler(mainCamParent.transform.rotation.x, 225, mainCamParent.transform.rotation.z))
        {
            Debug.Log(225);
            cameraPos = CameraPos.pos3;
        }
        else if (mainCamParent.transform.rotation == Quaternion.Euler(mainCamParent.transform.rotation.x, 315, mainCamParent.transform.rotation.z))
        {
            Debug.Log(315);
            cameraPos = CameraPos.pos4;
        }
    }

    private void Update()
    {
        Move();
    }
    public void Move()
    {
        ChecarCamera();
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direcao = Direction.D;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direcao = Direction.A;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            direcao = Direction.W;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            direcao = Direction.S;
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            return;
        }
        direcao = corrigirDirecao();
        Debug.Log("Direcao = " + direcao);
        //int newDirection = direction + (int)cameraPos;
        //Indice + (cameraPos)
        // W = 0
        // D = 1
        // S = 2
        // A = 3
        // 0 + (0) -> W
        // 0 + (3) -> A
        // 3 + (3) -> S
    }
    public Direction corrigirDirecao()
    {
        int num = (int)direcao + ((int)cameraPos);
        if (num > 3)
        {
            return (Direction)(num - 4);
        }
        else
        {
            return (Direction)num;
        }
    }
    public void trocarDirecao()
    {
        //Se negativo soma 360, se >=360, subtrai 360.
        //Cima 0, 0, 0
        //Esquerda 0, 0, 90
        //Baixo 0, 0, 180 ou 180, 0, 0
        //Direita 0, 0, 270
        //Frente 90, 0, 0
        //Costas 270, 0, 0

        if (faceAtual == FaceEnum.Cima)
        {
            if (vertical == 1) proximaFace = FaceEnum.Frente;
            if (vertical == -1) proximaFace = FaceEnum.Costas;
            if (horizontal == 1) proximaFace = FaceEnum.Direita;
            if (horizontal == -1) proximaFace = FaceEnum.Esquerda;
        }
    }
}
