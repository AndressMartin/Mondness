using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject camPivot;
    [SerializeField] GameObject personagem;
    float horizontal;
    float vertical;
    float velocidade = 5f;

    bool caindo;

    public enum FaceEnum { Cima, Baixo, Esquerda, Direita, Frente, Costas}
    public enum Direction { W, D, S, A}

    public FaceEnum faceAtual = FaceEnum.Cima;
    public FaceEnum proximaFace = FaceEnum.Frente;
    public Direction direcao = Direction.W;
    private bool pressionouUmAxis = true;
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

        //Se está parado
        if (transform.position == pontoEstatico) 
        { 
            WaitForInput(); 
        }
        //Se terminou de se movimentar
        if (transform.position == pontoMov.position)
        {
            pontoEstatico = pontoMov.position;
        }
        //Movimenta-se se o ponto seguinte tem um bloco para sustentar o player
        if (DetectBlocos.hitColliders.Length > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
            //TODO: Verificar se tem um bloco logo em frente ao player, já que ele não pode trombar com outro bloco.
        }
        //else
        //{
        //    pontoMov.position = pontoEstatico;
        //}
        else if (pressionouUmAxis) //Rotaciona o player por não ter encontrado nenhum bloco em frente
        {
            Debug.Log(DetectBlocos.hitColliders.Length);
            Rotacionar();
        }
    }

    private void Rotacionar()
    {
        Debug.Log("Rotacionar");
        transform.rotation = Quaternion.AngleAxis(90, transform.forward) * transform.rotation;
        pontoMov.position = transform.position;
        //pontoMov.rotation = transform.rotation;
        pressionouUmAxis = false;
    }

    public void WaitForInput()
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
        //pressionouUmAxis = true;
        OlharParaDirecao();
        if (DetectBlocos.hitColliders.Length > 0) Andar();
        else pontoMov.position = pontoEstatico;
    }

    private void OlharParaDirecao()
    {
        if (direcao == Direction.W)
        {
            transform.rotation = Quaternion.AngleAxis(0, transform.up);
        }
        if (direcao == Direction.D)
        {
            transform.rotation = Quaternion.AngleAxis(90, transform.up);
        }
        if (direcao == Direction.S)
        {
            transform.rotation = Quaternion.AngleAxis(180, transform.up);
        }
        if (direcao == Direction.A)
        {
            transform.rotation = Quaternion.AngleAxis(270, transform.up);
        }
    }

    private void Andar()
    {
        Debug.Log($"Andando em {direcao}");
        pontoMov.position += Vector3Int.FloorToInt(transform.forward * 1);
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
