using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Text textHealth;
    int health;

    public int GetHealth { get => health; }

    void Awake()
    {
        textHealth = GetComponent<Text>();
    }

    public void RefreshHealth(int newHealth)
    {
        if(newHealth >= 0)
        {
            health = newHealth;
            textHealth.text = $"{GetHealth}";
        }
    }
}
