
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Movement;

public class Player : Character
{

    Animator animator;
    NPC npc;

    void Awake()
    {
        animator = GetComponent<Animator>();
        canTalk = true;
        talking = false;
    }

    bool canTalk;
    bool btnTalk;
    bool talking;

    void Update()
    {
        if (!talking)
            base.Update();
        btnTalk = Input.GetButtonDown("Talk");
    }

    public override void Move()
    {
        base.Move();
        animator.SetFloat("move", Mathf.Abs(Movement.Axis.magnitude));
    }

    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("NPC"))
        {
            Debug.Log("Pues esto funciona");
            if (npc == null){
                npc = col.gameObject.GetComponent<NPC>();
                GameManager.instance.TextoInteractuar.SetActive(true);
                //GameManager.instance.MostrarTexto("Presiena \"E\" en el teclado o \"B\" en el mando, para hablar.");
            }
            if(btnTalk){
                if(canTalk){
                    canTalk = false;
                    talking = true;
                    GameManager.instance.TextoInteractuar.SetActive(false);
                    GameManager.instance.MostrarTexto(npc.Text);
                    GameManager.instance.Save();
                }else{
                    talking = false;
                    GameManager.instance.OcultarTexto();
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("NPC"))
        {
            if (npc != null)
                npc = null;
            GameManager.instance.OcultarTexto();
            GameManager.instance.TextoInteractuar.SetActive(false);
            canTalk = true;
        }
    }

}
