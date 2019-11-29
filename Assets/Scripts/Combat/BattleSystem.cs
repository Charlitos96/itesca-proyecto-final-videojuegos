using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour {

    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerTile;
    public Transform enemyTile;

    // Start is called before the first frame update
    void Start () {
        state = BattleState.START;
        SetupBattle ();
    }

    void SetupBattle () {
        GameObject pgo = Instantiate(playerPrefab, playerTile);
        GameObject ego = Instantiate(enemyPrefab, enemyTile);
    }

}