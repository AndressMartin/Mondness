using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAnimacaoPulo : MonoBehaviour
{
    private PlayerMove playerMove;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        animator = GetComponent<Animator>();
    }

    public void Pular()
    {
        animator.Play("Pulando");
    }

    public void FicarParado()
    {
        animator.Play("Parado");
    }

    public void FinalizarPulo()
    {
        playerMove.TerminarPulo();
    }
}
