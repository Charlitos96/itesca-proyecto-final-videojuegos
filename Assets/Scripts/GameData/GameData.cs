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


    public GameData()
    {

    }

    public GameData(Vector3 position, Quaternion dir)
    {
        this.position = position;
        this.dir = dir;
    }

    public Vector3 Position { get => position; set => position = value; }
    public Quaternion Dir { get => dir; set => dir = value; }
}
