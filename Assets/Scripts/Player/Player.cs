
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Movement;

public class Player : Character
{
/*
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    float rayDistance;*/
    [SerializeField]
    int jumpForce;
    [SerializeField]
    int maxJump;
    int currentJumps;
    bool alive;
    Rigidbody rigidBody;
    Animator animator;
    NPC npc;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        canTalk = true;
        talking = false;
        alive = true;
        currentJumps = 0;
        animator.SetBool("alive", alive); // 1
    }

    bool canTalk;
    bool btnTalk;
    bool talking;

    void Update()
    {
        if(alive){
            if (!talking){
                base.Update();
                Jump();
            }
        }else{
            Relive();
        }
        btnTalk = Input.GetButtonDown("Talk");
        //animator.SetBool("alive", alive);
    }

    public override void Move()
    {
        base.Move();
        animator.SetFloat("move", Mathf.Abs(Movement.Axis.magnitude));
    }
    
    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && maxJump > currentJumps)
        {
            //rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            currentJumps++;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //animator.SetTrigger("jump"); // Brinca muy feo
        }
    }

    public void Relive()
    {
        if(btnTalk)
        {
            Health = 80;
            alive = true;
            animator.SetBool("alive", alive); // 2
            GameManager.instance.RestartLevel();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(alive)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                currentJumps = 0;
                animator.SetBool("grounding", true);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(alive)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                animator.SetBool("grounding", false);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(alive)
        {
            if(col.CompareTag("Coin"))
            {
                //GameManager.instance.Save();
                Coin coin = col.GetComponent<Coin>();
                GameManager.instance.GetScore.AddPoints(coin.Points);
                Destroy(col.gameObject);
                //audioSource.PlayOneShot(GameManager.instance.CoinSound, 7f);
            }
            if(col.CompareTag("Damage"))
            {
                Damage damage = col.GetComponent<Damage>();
                Health -= damage.GetDamage;
                if(Health < 0)
                {
                    Health = 0;
                    alive = false;
                    animator.SetBool("alive", alive); // 3
                }
                animator.SetTrigger("damage");
                GameManager.instance.GetHealth.RefreshHealth(Health);
            }
            if(col.CompareTag("Portal"))
            {
                Debug.Log("Next Level!");
                SceneManager.LoadScene("Final");
            }
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
        if(alive)
        {
            if(col.CompareTag("NPC"))
            {
                if (npc == null){
                    npc = col.gameObject.GetComponent<NPC>();
                    GameManager.instance.TextoInteractuar.SetActive(true);
                    //GameManager.instance.MostrarTexto("Presiena \"E\" en el teclado o \"B\" en el mando, para hablar.");
                }
                if(btnTalk && alive){
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
        
    }
    void OnTriggerExit(Collider col)
    {
        if(alive)
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

}
