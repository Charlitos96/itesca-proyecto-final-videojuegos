using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour {

    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerTile;
    public Transform enemyTile;
    Unit playerUnit;
    Unit enemyUnit;
    public Text textoNombre;
    public Text textoDialogo;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    // Start is called before the first frame update
    void Start () {
        state = BattleState.START;
        StartCoroutine (SetupBattle ());
    }

    IEnumerator SetupBattle () {
        GameObject pgo = Instantiate (playerPrefab, playerTile);
        playerUnit = pgo.GetComponent<Unit> ();
        GameObject ego = Instantiate (enemyPrefab, enemyTile);
        enemyUnit = ego.GetComponent<Unit> ();
        textoNombre.text = playerUnit.unitName;
        textoDialogo.text = "Inicia la batalla...";
        playerHUD.SetHUD (playerUnit);
        enemyHUD.SetHUD (enemyUnit);
        yield return new WaitForSeconds (2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn ();
    }

    void PlayerTurn () {
        textoDialogo.text = "Escoge una acción...";
    }

    public void onAttackBtn () {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine (Atacar ());
    }

    IEnumerator Atacar () {
        bool isDead = false;
        System.Random rnd = new System.Random ();
        int n = rnd.Next (1, 101);
        if (n >= 90) {
            textoDialogo.text = "Ataque fallido...";
        } else {
            isDead = enemyUnit.recibirDmg (playerUnit.dmg);
            enemyHUD.setHP (enemyUnit.initialHp);
            textoDialogo.text = "¡Ataque realizado con éxito!";
        }
        yield return new WaitForSeconds (2f);
        if (isDead) {
            state = BattleState.WON;
            EndBattle ();
        } else {
            state = BattleState.ENEMYTURN;
            StartCoroutine (EnemyTurn ());
        }
    }

    void EndBattle () {
        if (state == BattleState.WON) {
            textoDialogo.text = "¡GANASTE LA BATALLA!";
        } else {
            textoDialogo.text = "Perdiste la batalla...";
        }
    }

    IEnumerator EnemyTurn () {
        textoDialogo.text = "Turno de " + enemyUnit.unitName;
        yield return new WaitForSeconds(2f);
    }

}