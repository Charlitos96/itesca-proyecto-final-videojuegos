using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Enemy enemy;

    void Update()
    {
        if(player.Health > 0 && enemy.Health > 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(!player.Attacked)
                {
                    enemy.Health--;
                    player.Attacked = true;
                    enemy.Attacked = false;
                }
                else
                {
                    player.Health--;
                    enemy.Attacked = true;
                    player.Attacked = false;
                }
            }
            Debug.Log($"Player health {player.Health}");
            Debug.Log($"Enemy health {enemy.Health}");
        }
        else
        {
            if(enemy.Health <= 0 && player.Health > 0)
            {
                Debug.Log("you won the combat.");
            }
            if(enemy.Health > 0 && player.Health <= 0)
            {
                Debug.Log("you lost the combat.");
            }
        }
    }

}
