using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    int power;
    public int Power { get => power; set => power = value; }

    void Update()
    {
        
    }
}
