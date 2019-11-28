using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    /*[SerializeField]
    float posX;
    [SerializeField]
    float posY;*/

    [SerializeField]
    Vector3 position;
    [SerializeField]
    Quaternion dir;

    //public float PosX { get => posX; set => posX = value; }
    //public float PosY { get => posY; set => posY = value; }

    /*public GameData(float posX, float posY)
    {
        this.posX = posX;
        this.posY = posY;
    }*/

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
