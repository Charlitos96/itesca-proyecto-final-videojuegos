using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Movement;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected float moveSpeed;

    protected float maxHealth;

    bool attacked;
    bool btnRun;

    public int Health { get => health; set => health = value; }
    public bool Attacked { get => attacked; set => attacked = value; }

    protected void Update()
    {
        //btnRun = Input.GetButtonDown("Fire3");
        btnRun = Input.GetButton("Fire3");
        Move();
    }

    public virtual void Move()
    {
        if(btnRun)
        {
            Debug.Log("Running: "+(moveSpeed * 2));
            Movement.Move3DTopDown(transform, (moveSpeed * 2), Movement.AxisDelta);
        }
        else
        {
            Debug.Log("Walking: "+(moveSpeed));
            Movement.Move3DTopDown(transform, moveSpeed, Movement.AxisDelta);
        }
    }

}
