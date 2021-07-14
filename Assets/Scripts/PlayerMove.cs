using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    FMOD.Studio.EventInstance passosSfx;
    FMOD.Studio.EventInstance puloSfx;
    FMOD.Studio.EventInstance quedaSfx;
    [SerializeField] GameObject personagem;
    [SerializeField] GameObject teleportTrail;
    private GameObject myTrail;
    float horizontal;
    float vertical;
    float movVel = 5f;
    float quedaVel = 13f;
    float rotVel = 90f;

    float tempoCaindo = 0;
    float maxTempoCaindo = 0.9f;
    Vector3 velF;

    UnityEvent DerrubarCuboAnterior;
    public static UnityEvent startIdle;
    public static UnityEvent startRun;
    public static UnityEvent flutuando;

    public enum Direction { W, D, S, A}
    
    public enum State { Parado, Andando, ViraFace, ViraCorpo, Caindo, Pulando, TerminandoQueda, Teleportando, Flutuando, Esperando}
    public State estado = State.Parado;
    public State estadoAnterior = State.Parado; //TODO: SETTAR E RESETTAR

    public Direction direcao = Direction.W;
    public Direction direcaoIni;
    public Direction direcaoAnterior = Direction.S;
    private Enums.CameraPos myCamPos = Enums.CameraPos.pos1;
    public Vector3 pontoEstatico;
    public Vector3 teleportTarget;
    public Transform pontoMov;
    private bool rotacionando;
    float maxRotTime = 1f;
    float rotTime;
    private Quaternion targetToRotate;

    Vector3 posIni;
    Quaternion rotIni;
    Quaternion perRotIni;

    private bool andandoNoGelo = false;
    private bool gameStarted;

    private void Awake()
    {
        if (DerrubarCuboAnterior == null)
        {
            DerrubarCuboAnterior = new UnityEvent();
        }
        if (flutuando == null)
        {
            flutuando = new UnityEvent();
        }
        if (startIdle == null)
        {
            startIdle = new UnityEvent();
        }
        if (startRun == null)
        {
            startRun = new UnityEvent();
        }
        pontoMov.position = transform.position;
        pontoEstatico = pontoMov.position;
    }

    private void Start()
    {
        SetParamsOriginais();
        CuboManager.ResetarCena.AddListener(ResetParams);
        StageManage.puzzlesLoaded.AddListener(comecar);
        passosSfx = RuntimeManager.CreateInstance("event:/sfx/passos");
        puloSfx = RuntimeManager.CreateInstance("event:/sfx/salto_pulo");
        quedaSfx = RuntimeManager.CreateInstance("event:/sfx/salto_queda");
        velF = new Vector3(0, 0, 0);
    }

    private void comecar()
    {
        gameStarted = true;
    }

    private void SetParamsOriginais()
    {
        posIni = transform.position;
        rotIni = transform.rotation;
        perRotIni = personagem.transform.rotation;
        estado = State.Parado;
        direcaoIni = direcao;
    }

    private void ResetParams()
    {
        transform.position = posIni;
        transform.rotation = rotIni;
        personagem.transform.rotation = perRotIni;
        estado = State.Parado;
        estadoAnterior = State.Parado;
        pontoEstatico = transform.position;
        pontoMov.position = transform.position;
        direcao = direcaoIni;
    }
    private void Update()
    {
        if (gameStarted != true) return;
        //Se está parado
        if (estado == State.Parado)
        {
            if (estadoAnterior != estado)
            {
                startIdle.Invoke();
                estadoAnterior = estado;
            }
            if (transform.position == pontoEstatico && CameraRotate.rotacionando == false)
            {
                WaitForInput();
            }
        }
        //Movimenta-se se o ponto seguinte tem um bloco para sustentar o player
        else if (estado == State.Andando)
        {
            if (estadoAnterior != estado)
            {
                RuntimeManager.AttachInstanceToGameObject(passosSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
                passosSfx.start();
                startRun.Invoke();
                estadoAnterior = estado;
            }
            transform.position = Vector3.MoveTowards(transform.position, pontoMov.position, movVel * Time.deltaTime);
            if (transform.position == pontoMov.position)
            {
                //CorrectPositions();
                DerrubarCuboAnterior.Invoke();
                pontoEstatico = pontoMov.position;
                estado = State.Parado;
            }
            
        }
        else if (estado == State.ViraCorpo)
        {
            if (!rotacionando)
            {
                RuntimeManager.AttachInstanceToGameObject(passosSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
                passosSfx.start();
                rotTime = maxRotTime;
            }
            if (rotTime > 0 ) Rotacionar();
            else
            {
                CorrectRotation();
                rotTime = 0;
                targetToRotate = transform.rotation;
                estado = State.Parado;
                rotacionando = false;
            }
        }
        else if (estado == State.Pulando)
        {
            Debug.Log("Pulando");
            AvisarCubo();
            StartCoroutine(Pulo());
            estado = State.Esperando;

        }
        else if (estado == State.Caindo)
        {
            //Debug.Log("Caindo");
            pontoMov.GetComponent<DetectBlocos>().MyCollisions();
            if (DetectBlocos.hitColliders.Length <= 0)
            {
                Cair();
                if (tempoCaindo > maxTempoCaindo)
                {
                    Flutuar();
                }
            }
            else
            {
                if(DetectBlocos.hitColliders[0].GetComponent<Bloco>().tipo != Bloco.TipoBloco.Nuvem)
                {
                    if (DetectBlocos.hitColliders[0].GetComponent<Bloco>().caindo == false)
                    {
                        if (DetectBlocos.hitColliders[0].GetComponent<Bloco>().flutuando == false)
                        {
                            pontoMov.position = DetectBlocos.hitColliders[0].transform.position;
                            estado = State.TerminandoQueda;
                        }
                        else
                        {
                            Flutuar();
                        }
                    } 
                }
                else
                {
                    Cair();
                    if (tempoCaindo > maxTempoCaindo)
                    {
                        Flutuar();
                    }
                }
            }
        }
        else if (estado == State.TerminandoQueda)
        {
            transform.position = Vector3.MoveTowards(transform.position, pontoMov.position, 60f * Time.deltaTime);
            if (transform.position == pontoMov.position)
            {
                pontoEstatico = pontoMov.position;
                estado = State.Parado;
            }
        }
        else if (estado == State.Teleportando)
        {
            Debug.Log("Teleportando");
            transform.position = Vector3.MoveTowards(transform.position, teleportTarget, 15f * Time.deltaTime);
            transform.Rotate(transform.up * 60f * Time.deltaTime);
            transform.Rotate(transform.forward * 60f * Time.deltaTime);
            transform.Rotate(transform.right * 60f * Time.deltaTime);
            if (transform.position == teleportTarget)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                pontoMov.position = transform.position;
                pontoEstatico = transform.position;
                StartCoroutine(DestroyTrail());
                SetParamsOriginais();
                Debug.Log("Fim do Teleporte");
            }
        }
        else if (estado == State.Flutuando)
        {
            personagem.transform.Rotate(velF * Time.deltaTime);
            transform.Rotate((velF / 4) * Time.deltaTime);
        }

        if (estado == State.Parado)
        {
            CameraRotate.AtualizarJogadorParado(true);
        }
        else
        {
            CameraRotate.AtualizarJogadorParado(false);
        }
    }

    private IEnumerator DestroyTrail()
    {
        yield return new WaitForSeconds(1f);
        Destroy(myTrail);
    }

    private void CreateTrail()
    {
        myTrail = Instantiate(teleportTrail, transform.GetChild(0).position, transform.GetChild(0).rotation, transform);
    }

    private void Cair()
    {
        pontoMov.position += transform.up * -1 * quedaVel * Time.deltaTime;
        transform.position = pontoMov.position;
        tempoCaindo += Time.deltaTime;
    }

    private IEnumerator Pulo()
    {
        //TODO: ANIMACAO DE PULO!
        RuntimeManager.AttachInstanceToGameObject(puloSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
        puloSfx.start();
        yield return new WaitForSeconds(/*tamanho Animação*/.6f);
        Debug.Log("Pulo Coroutine after yield");
        RuntimeManager.AttachInstanceToGameObject(quedaSfx, GetComponent<Transform>(), GetComponent<Rigidbody>());
        quedaSfx.start();
        DerrubarCuboAnterior.Invoke();
        DerrubarCuboAnterior.RemoveAllListeners();
        tempoCaindo = 0;
        estado = State.Caindo;
    }

    void Rotacionar()
    {
        Debug.Log("Personagem.transform.right = " + personagem.transform.right);
        Debug.Log("rotVel = " + rotVel);
        rotTime -= Time.deltaTime;
        transform.Rotate(personagem.transform.right * (rotVel * Time.deltaTime), Space.World);
        //if (transform.rotation == targetToRotate)
        //{
        //    rotTime = 0;
        //    pontoMov.position = transform.position;
        //}
        if (!rotacionando) Debug.LogWarning("END: " + targetToRotate);
        rotacionando = true;
    }

    private void CorrectRotation()
    {
        var vec = transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        transform.eulerAngles = vec;
    }
    private void CorrectPositions(Vector3 pos)
    {
        Vector3 tmp = pos;
        tmp.x = Mathf.Round(tmp.x);
        tmp.y = Mathf.Round(tmp.y);
        tmp.z = Mathf.Round(tmp.z);
        pos = tmp;
    }

    public void WaitForInput()
    {
        direcaoAnterior = direcao;
        myCamPos = CameraRotate.cameraPos;
        //Debug.LogWarning(myCamPos);
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Press Space");
            estado = State.Pulando;
            return;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direcao = Direction.A;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direcao = Direction.D;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            direcao = Direction.W;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            direcao = Direction.S;
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            return;
        }
        AvisarCubo();
        direcao = corrigirDirecao();
        OlharParaDirecao();
        Andar();
    }

    private void AvisarCubo()
    {
        DerrubarCuboAnterior.RemoveAllListeners();
        if (DetectBlocos.hitColliders.Length > 0)
        {
            DerrubarCuboAnterior.AddListener(DetectBlocos.hitColliders[0].GetComponent<Bloco>().ChecarQueda);
            DetectBlocos.hitColliders[0].GetComponent<Bloco>().PegarDir(transform.up * -1);
        }
    }

    private void OlharParaDirecao()
    {
        int num = (int)direcaoAnterior - (int)direcao;
        //Debug.Log($"{direcaoAnterior} - {direcao} = {Math.Abs((int)direcaoAnterior - (int)direcao)} e sem modulo = {direcaoAnterior - direcao}");
        personagem.transform.Rotate(0, 90 * num, 0);
    }

    private void Andar()
    {
        //Debug.Log($"Colocando {pontoMov.position} de rotação {pontoMov.rotation} em {direcao} a {transform.forward}");
        pontoMov.position += personagem.transform.forward * 1;

        pontoMov.GetComponent<DetectBlocos>().MyCollisions();
        if (DetectBlocos.hitColliders.Length > 0 && DetectBlocos.hitColliders[0].GetComponent<Bloco>().tipo != Bloco.TipoBloco.Nuvem)
        {
            if(DetectBlocos.hitColliders[0].GetComponent<Bloco>().caindo == false && DetectBlocos.hitColliders[0].GetComponent<Bloco>().flutuando == false)
            {
                if(DetectBlocos.hitColliders[0].GetComponent<Bloco>().tipo != Bloco.TipoBloco.Gelo)
                {
                    Debug.LogWarning(DetectBlocos.hitColliders.Length);
                    pontoMov.position += personagem.transform.up * 1;
                    pontoMov.GetComponent<DetectBlocos>().MyCollisions();
                    if (!(DetectBlocos.hitColliders.Length > 0))
                    {
                        Debug.LogWarning(DetectBlocos.hitColliders.Length);
                        pontoMov.position += personagem.transform.up * -1;
                        pontoMov.GetComponent<DetectBlocos>().MyCollisions();
                        if (DetectBlocos.hitColliders.Length > 0)
                        {
                            pontoMov.position = DetectBlocos.hitColliders[0].transform.position;
                            estado = State.Andando;
                        }
                    }
                    else
                    {
                        pontoMov.position = pontoEstatico;
                    }
                }
                else
                {
                    andandoNoGelo = true;

                    while(andandoNoGelo == true)
                    {
                        pontoMov.position += personagem.transform.up * 1;
                        pontoMov.GetComponent<DetectBlocos>().MyCollisions();
                        if (!(DetectBlocos.hitColliders.Length > 0))
                        {
                            pontoMov.position += personagem.transform.up * -1;
                            pontoMov.position += personagem.transform.forward * 1;

                            pontoMov.GetComponent<DetectBlocos>().MyCollisions();
                            if (!(DetectBlocos.hitColliders.Length > 0))
                            {
                                pontoMov.position += personagem.transform.forward * -1;

                                pontoMov.GetComponent<DetectBlocos>().MyCollisions();
                                if (DetectBlocos.hitColliders.Length > 0)
                                {
                                    pontoMov.position = DetectBlocos.hitColliders[0].transform.position;
                                    andandoNoGelo = false;
                                }
                            }
                            else
                            {
                                if(DetectBlocos.hitColliders[0].GetComponent<Bloco>().tipo != Bloco.TipoBloco.Gelo)
                                {
                                    pontoMov.position = DetectBlocos.hitColliders[0].transform.position;
                                    andandoNoGelo = false;
                                }
                            }
                        }
                        else
                        {
                            pontoMov.position += personagem.transform.up * -1;
                            pontoMov.position += personagem.transform.forward * -1;

                            pontoMov.GetComponent<DetectBlocos>().MyCollisions();
                            if (DetectBlocos.hitColliders.Length > 0)
                            {
                                pontoMov.position = DetectBlocos.hitColliders[0].transform.position;
                                andandoNoGelo = false;
                            }
                        }
                    }

                    estado = State.Andando;
                }
            }
            else
            {
                pontoMov.position = pontoEstatico;
            }
        }
        else
        {
            pontoMov.position += personagem.transform.up * 1;
            pontoMov.GetComponent<DetectBlocos>().MyCollisions();
            if (!(DetectBlocos.hitColliders.Length > 0))
            {
                Debug.LogWarning(DetectBlocos.hitColliders.Length);
                estado = State.ViraCorpo;
            }
            pontoMov.position = pontoEstatico;
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


    public void Teleport(Vector3 position)
    {
        estado = State.Teleportando;
        tempoCaindo = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        targetToRotate = transform.rotation;
        rotacionando = false;
        CreateTrail();
        teleportTarget = position;
    }

    private void Flutuar()
    {
        velF = new Vector3(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f));
        estado = State.Flutuando;
        flutuando.Invoke();
    }
}
