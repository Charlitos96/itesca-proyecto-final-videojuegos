using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    [SerializeField]
    Vector3 position;
    [SerializeField]
    Quaternion dir;
    [SerializeField]
    int score;
    [SerializeField]
    int health;


    public GameData()
    {

    }

    public GameData(Vector3 position, Quaternion dir, int score, int health)
    {
        this.position = position;
        this.dir = dir;
        this.score = score;
        this.health = health;
    }

    public Vector3 Position { get => position; set => position = value; }
    public Quaternion Dir { get => dir; set => dir = value; }
    public int Score { get => score; set => score = value; }
    public int Health { get => health; set => health = value; }
}
