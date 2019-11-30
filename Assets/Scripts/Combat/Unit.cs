using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public string unitName;
    public int hp;
    public int initialHp;
    public int dmg;
    public int dfs;
    public int pos;

    public bool recibirDmg (int dmg) {
        initialHp -= dmg;
        if (initialHp <= 0) {
            return true;
        }
        return false;
    }
}