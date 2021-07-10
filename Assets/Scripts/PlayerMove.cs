using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]GameObject camPivot;
    float horizontal;
    float vertical;
    float velocidade = 5f;

    bool caindo;

    public enum FaceEnum { Cima, Baixo, Esquerda, Direita, Frente, Costas}
    public enum Direction { W, D, S, A}

    public FaceEnum faceAtual = FaceEnum.Cima;
    public FaceEnum proximaFace = FaceEnum.Frente;
    public Direction direcao = Direction.W;
    private Enums.CameraPos myCamPos = Enums.CameraPos.pos1;
    public Vector3 pontoEstatico;
    public Transform pontoMov;
    private void Awake()
    {
        pontoMov.position = transform.position;
        pontoEstatico = pontoMov.position;
    }
    private void Update()
    {
        if (transform.position == pontoEstatico) Move();
        if (transform.position == pontoMov.position) pontoEstatico = pontoMov.position;
        transform.position = Vector3.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
    }
    public void Move()
    {
        myCamPos = CameraRotate.cameraPos;
        Debug.LogWarning(myCamPos);
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
        Andar();
    }

    private void Andar()
    {
        Debug.Log($"Andando em {direcao}");
        if (direcao == Direction.W)
        {
            pontoMov.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        if (direcao == Direction.D)
        {
            pontoMov.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        if (direcao == Direction.S)
        {
            pontoMov.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
        if (direcao == Direction.A)
        {
            pontoMov.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
    }

    //int newDirection = direction + (int)cameraPos;
    //Indice + (cameraPos)
    // W = 0
    // D = 1
    // S = 2
    // A = 3
    // 0 + (0) -> W
    // 0 + (3) -> A
    // 3 + (3) -> S
    public Direction corrigirDirecao()
    {
        int num = (int)direcao + ((int)myCamPos);
        if (num > 3)
        {
            return (Direction)(num - 4);
        }
        else
        {
            return (Direction)num;
        }
    }
    public void TrocarFace()
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
