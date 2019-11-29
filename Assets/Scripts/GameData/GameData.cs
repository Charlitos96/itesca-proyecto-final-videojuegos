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
    int healt;


    public GameData()
    {

    }

    public GameData(Vector3 position, Quaternion dir, int score, int healt)
    {
        this.position = position;
        this.dir = dir;
        this.score = score;
        this.healt = healt;
    }

    public Vector3 Position { get => position; set => position = value; }
    public Quaternion Dir { get => dir; set => dir = value; }
    public int Score { get => score; set => score = value; }
    public int Healt { get => healt; set => healt = value; }
}
