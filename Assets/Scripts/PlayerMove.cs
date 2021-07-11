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
    float movVel = 5f;
    float rotVel = 90f;
    float rotsmooth = 30f;
    bool caindo;

    public enum FaceEnum { Cima, Baixo, Esquerda, Direita, Frente, Costas}
    public enum Direction { W, D, S, A}

    public enum State { parado, andar, virarY, virarCorpo}

    public FaceEnum faceAtual = FaceEnum.Cima;
    public FaceEnum proximaFace = FaceEnum.Frente;
    public Direction direcao = Direction.W;
    public Direction direcaoAnterior = Direction.S;
    public State estado = State.parado;
    private bool pressionouUmAxis = true;
    private Enums.CameraPos myCamPos = Enums.CameraPos.pos1;
    public Vector3 pontoEstatico;
    public Transform pontoMov;
    private bool rotacionando;
    float maxRotTime = 1f;
    float rotTime;
    private Vector3 currentEulerAngles;
    float totalRot;
    private Quaternion targetToRotate;

    private void Awake()
    {
        pontoMov.position = transform.position;
        pontoEstatico = pontoMov.position;
    }
    private void Update()
    {
        //Se está parado
        if (estado == State.parado)
        {
            if (transform.position == pontoEstatico)
            {
                WaitForInput();
            }
            //Se terminou de se movimentar
            
        }
        //Movimenta-se se o ponto seguinte tem um bloco para sustentar o player
        else if (estado == State.andar)
        {
            transform.position = Vector3.MoveTowards(transform.position, pontoMov.position, movVel * Time.deltaTime);
            if (transform.position == pontoMov.position)
            {
                pontoEstatico = pontoMov.position;
                estado = State.parado;
            }
            //TODO: Verificar se tem um bloco logo em frente ao player, já que ele não pode trombar com outro bloco.
        }
        if (estado == State.virarCorpo)
        {
            if (!rotacionando)
            {
                rotTime = maxRotTime;
            }
            if (rotTime > 0 ) Rotacionar2();
            else
            {
                targetToRotate = transform.rotation;
                estado = State.parado;
                rotacionando = false;
            }
        }
    }

    void Rotacionar2()
    {
        rotTime -= Time.deltaTime;
        transform.Rotate(personagem.transform.right * (rotVel * Time.deltaTime));
        if (transform.rotation == targetToRotate)
        {
            rotTime = 0;
            pontoMov.position = transform.position;
        }
        if (!rotacionando) Debug.LogWarning("END: " + targetToRotate);
        rotacionando = true;
    }
    public void WaitForInput()
    {
        direcaoAnterior = direcao;
        myCamPos = CameraRotate.cameraPos;
        //Debug.LogWarning(myCamPos);
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direcao = Direction.A;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direcao = Direction.D;
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
        OlharParaDirecao();
        Andar();
    }

    private void OlharParaDirecao()
    {
        int num = (int)direcaoAnterior - (int)direcao;
        //Debug.Log($"{direcaoAnterior} - {direcao} = {Math.Abs((int)direcaoAnterior - (int)direcao)} e sem modulo = {direcaoAnterior - direcao}");
        personagem.transform.Rotate(0, 90 * num, 0);
    }

    private void Andar()
    {
        Debug.Log($"Colocando {pontoMov.position} de rotação {pontoMov.rotation} em {direcao} a {transform.forward}");
        pontoMov.position += personagem.transform.forward * 1;
        /*
        if (direcao == Direction.D)
        {
            pontoMov.position -= transform.right * 1;
        }
        if (direcao == Direction.A)
        {
            pontoMov.position += transform.right * 1;
        }
        if (direcao == Direction.W)
        {
            pontoMov.position += transform.forward * 1;
        }
        if (direcao == Direction.S)
        {
            pontoMov.position -= transform.forward * 1;
        }
        */
        pontoMov.GetComponent<DetectBlocos>().MyCollisions();
        if (DetectBlocos.hitColliders.Length > 0)
        {
            Debug.LogWarning(DetectBlocos.hitColliders.Length);
            estado = State.andar;
        }
        else
        {
            pontoMov.position = pontoEstatico;
            estado = State.virarCorpo;
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
        int num = (int)direcao - ((int)myCamPos);
        if (num > 3)
        {
            return (Direction)(num - 4);
        }
        else if (num < 0)
        {
            return (Direction)(num + 4);
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
