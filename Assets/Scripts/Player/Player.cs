
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Movement;

public class Player : Character
{

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    float rayDistance;
    [SerializeField]
    int jumpForce;
    Rigidbody rigidBody;
    Animator animator;
    NPC npc;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
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
        Jump();
    }

    public override void Move()
    {
        base.Move();
        animator.SetFloat("move", Mathf.Abs(Movement.Axis.magnitude));
    }
    
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        /*
        //grounding
        RaycastHit hit = Physics.Raycast(
            transform.position,
            -transform.up, 
            rayDistance,
            groundLayer
        );
        if(hit.collider)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        */
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Coin"))
        {
            //GameManager.instance.Save();
            Coin coin = col.GetComponent<Coin>();
            GameManager.instance.GetScore.AddPoints(coin.Points);
            Destroy(col.gameObject);
            //audioSource.PlayOneShot(GameManager.instance.CoinSound, 7f);
        }
        if(col.CompareTag("Enemy"))
        {
            Enemy enemy = col.GetComponent<Enemy>();
            Health -= enemy.Power;
        }
        /*
        if(col.CompareTag("death"))
        {
            Destroy(gameObject);
            //SceneManager.LoadScene("level01");
        }
        if(col.CompareTag("end"))
        {
            Debug.Log("Level ended");
            //SceneManager.LoadScene("level02");
        }
        */
    }
    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("NPC"))
        {
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
