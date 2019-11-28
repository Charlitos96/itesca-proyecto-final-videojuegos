using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Movement;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float health;

    [SerializeField]
    protected float moveSpeed;

    bool attacked;

    public float Health { get => health; set => health = value; }
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
