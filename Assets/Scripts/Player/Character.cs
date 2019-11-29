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

    public int Health { get => health; set => health = value; }
    public bool Attacked { get => attacked; set => attacked = value; }

    protected void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        Movement.Move3DTopDown(transform, moveSpeed, Movement.AxisDelta);
    }

}
