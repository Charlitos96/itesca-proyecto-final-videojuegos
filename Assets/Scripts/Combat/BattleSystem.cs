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

    // Start is called before the first frame update
    void Start () {
        state = BattleState.START;
        SetupBattle ();
    }

    void SetupBattle () {
        GameObject pgo = Instantiate(playerPrefab, playerTile);
        playerUnit = pgo.GetComponent<Unit>();
        GameObject ego = Instantiate(enemyPrefab, enemyTile);
        enemyUnit = pgo.GetComponent<Unit>();

        textoNombre.text = playerUnit.unitName;
        textoDialogo.text = "Inicia la batalla...";
    }

}