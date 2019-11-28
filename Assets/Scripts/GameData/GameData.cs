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

    public GameData(Vector2 position)
    {
        this.position = position;
    }

    public Vector2 Position { get => position; set => position = value; }
}
