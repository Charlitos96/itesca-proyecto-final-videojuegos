using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    [SerializeField]
    string text;

    public string Text { get => text; }

    void Awake()
    {
        
    }
    void Update() { }

    public void Hablar()
    {
        Debug.Log("Hola tio, como estaS?");
    }
}
