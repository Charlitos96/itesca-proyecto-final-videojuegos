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
    public bool defended;

    public bool recibirDmg (int dmg) {
        if (defended) {
            if (dmg > dfs) {
                initialHp -= (dmg - dfs);
            }
        } else {
            initialHp -= dmg;
        }
        if (initialHp <= 0) {
            return true;
        }
        return false;
    }

    public void Descubrirse () {
        defended = false;
    }

    public void Cubrirse () {
        defended = true;
    }

    public void Curarse (int pts) {
        if (initialHp + pts > hp) {
            initialHp = hp;
        } else {
            initialHp += pts;
        }
    }
}