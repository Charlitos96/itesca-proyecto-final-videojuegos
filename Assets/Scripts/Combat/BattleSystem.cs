using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este enum define los diferentes estados por los que puede pasar la batalla
//START es en donde se inicializa todo, también se muestra un "Inicia la batalla..."
//PLAYERTURN es representa el turno del jugador para elegir una acción
//ENEMYTURN representa el turno del enemigo para elegir una acción al azar contra el jugador
//WON representa el estado donde el jugador "mató" al enemigo
//LOST es el estado en donde el enemigo "mató" al jugador
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour {

    public BattleState state; //Aquí se crea una instancia del enum, que va a definir en qué estado se encuentra la batalla
    public GameObject playerPrefab; //Este es el prefab que va a manejar la batalla y que represantará al jugador: Este prefab contiene el modelo, y el script Unit correspondiente.
    public GameObject enemyPrefab; //Esta es exactamente igual a playerPrefab, pero para el enemigo
    public Transform playerTile; //Este es el Transform de la plataforma en donde aparece el jugador, sirve para calcular la posición automáticamente
    public Transform enemyTile; //Este es igual que el playerTile, pero para el enemigo
    Unit playerUnit; //Esta es una referencia al script Unit que contiene el jugador
    Unit enemyUnit; //Y esta es la de Unit del enemigo
    public Text textoNombre; //Este es un texto para el panel de la izquierda de la UI
    public Text textoDialogo; //Este es el texto para el panel de diálogo o de descripción.
    public BattleHUD playerHUD; //El playerHUD es el UI que debe estar encima del jugador
    public BattleHUD enemyHUD; //Y este es el que debe estar encima del enemigo.

    void Start () {
        state = BattleState.START; //Se inicializa el estado en START
        StartCoroutine (SetupBattle ()); //Se crea una corutina (Una especie de hilo), que inicializará la batalla
    }

    IEnumerator SetupBattle () {
        GameObject pgo = Instantiate (playerPrefab, playerTile); //Aquí se hace aparecer al prefab del jugador como hijo (Encima) de el tile del jugador y se asigna el valor a un GameObject
        playerUnit = pgo.GetComponent<Unit> (); //Se extrae el script Unit del objeto jugador, y se utiliza para inicializar el atributo playerUnit
        GameObject ego = Instantiate (enemyPrefab, enemyTile);
        enemyUnit = ego.GetComponent<Unit> ();
        textoNombre.text = playerUnit.unitName; //Se utiliza la clase plana playerUnit ya inicializada para obtener el nombre del jugador
        textoDialogo.text = "Inicia la batalla..."; //Se inicializa el texto del cuádro de diálogo
        playerHUD.SetHUD (playerUnit); //Este método inicializa el HUD con los datos de la clase plana playerUnit
        enemyHUD.SetHUD (enemyUnit);
        yield return new WaitForSeconds (2f); //Aquí se espera 2 segundos para que el jugador pueda entender con tiempo qué está sucediendo
        playerUnit.defended = false; //Se inicializa el booleano en falso para decir que el jugador no se está defendiendo.
        enemyUnit.defended = false;
        state = BattleState.PLAYERTURN; //Después de inicializar todo se pasa al estado PLAYERTURN, en donde el jugador deberá decidir qué acción llevar a cabo.
        PlayerTurn ();
    }

    void PlayerTurn () {
        playerUnit.Descubrirse (); //Cada que inicia este método significa que se acaba de cambiar a este estado por lo que es necesario hacer que el jugador ya no se proteja más en caso de que lo estuviera haciendo.
        textoDialogo.text = "Escoge una acción..."; //Se le dice al usuario que escoga una acción.
    }

    public void onAttackBtn () { //Este método espera a que se presione el botón de ataque.
        if (state != BattleState.PLAYERTURN) { //Solo hará algo si el estado está en PLAYERTURN
            return;
        }
        StartCoroutine (Atacar ()); //En caso de que sí, se comienza una corutina para atacar.
    }

    public void onDefendBtn () {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine (Defender ());
    }

    public void onHealBtn () {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine (Curar ());
    }

    IEnumerator Curar() {
        textoDialogo.text = "Te has curado...";
        playerUnit.Curarse(20);
        playerHUD.setHP (playerUnit.initialHp);
        yield return new WaitForSeconds (2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine (EnemyTurn ());
    }

    IEnumerator Defender () {
        textoDialogo.text = "Te has defendido...";
        playerUnit.Cubrirse ();
        yield return new WaitForSeconds (2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine (EnemyTurn ());
    }

    IEnumerator Atacar () {
        bool isDead = false; //Este es un booleano para saber si el enemigo murió durante el ataque o no
        System.Random rnd = new System.Random (); //Se genera una instancia de Random llamada rnd
        int n = rnd.Next (1, 101); //Utilizando rnd se crea un número entero random del 1 al 100
        if (n >= 90) { //Si el número es mayor o igual a 90 el jugador va a fallar el ataque.
            textoDialogo.text = "Ataque fallido...";
        } else {
            isDead = enemyUnit.recibirDmg (playerUnit.dmg); //En caso de que sea menor a 90 entonces se realiza un ataque exitoso, con el cual se llama a este método para que el enemigo reciba daño
            enemyHUD.setHP (enemyUnit.initialHp); //Se actualizan los puntos de vida en el HUD del enemigo.
            if (enemyUnit.defended) { //Si el enemigo se estaba defendiendo
                textoDialogo.text = "¡Ataque defendido!";
            } else {
                textoDialogo.text = "¡Ataque realizado con éxito!";
            }
        }
        yield return new WaitForSeconds (2f); //Se esperan 2 segundos
        if (isDead) { //Si el enemigo murió entonces se pasa al estado de WON
            state = BattleState.WON;
            EndBattle ();
        } else { //En caso de que no se pasa a ENEMYTURN y es el turno del enemigo
            state = BattleState.ENEMYTURN;
            StartCoroutine (EnemyTurn ());
        }
    }

    void EndBattle () {
        playerUnit.Descubrirse (); //Se descubren los jugadores por seguridad
        enemyUnit.Descubrirse ();
        if (state == BattleState.WON) { //Si el estado es WON entonces se imprime que se ganó la batalla, de otra forma se imprime que se perdió
            textoDialogo.text = "¡GANASTE LA BATALLA!";
        } else {
            textoDialogo.text = "Perdiste la batalla...";
        }
    }

    IEnumerator EnemyTurn () {
        enemyUnit.Descubrirse ();
        bool isDead = false;
        textoDialogo.text = "Turno de " + enemyUnit.unitName;
        yield return new WaitForSeconds (2f);
        System.Random rnd = new System.Random ();
        int n = rnd.Next (1, 151); //Se genera ahora del 1 al 150, 100 para abajo significa que atacará, 125 para abajo que se defendera y 125 para arriba que se va a curar
        if (n <= 100) {
            //Ataca
            int n2 = rnd.Next (1, 101);
            if (n2 <= 90) {
                isDead = playerUnit.recibirDmg (enemyUnit.dmg);
                if (playerUnit.defended) {
                    textoDialogo.text = "¡Ataque defendido!";
                } else {
                    textoDialogo.text = "!" + enemyUnit.unitName + "a taca!";
                }
            } else {
                textoDialogo.text = "!" + enemyUnit.unitName + " ha fallado!";
            }
            playerHUD.setHP (playerUnit.initialHp);
        } else if (n <= 125) {
            textoDialogo.text = enemyUnit.unitName + " se ha cubierto...";
            enemyUnit.Cubrirse ();
        } else {
            textoDialogo.text = enemyUnit.unitName + " se ha curado...";
            enemyUnit.Curarse (20);
            enemyHUD.setHP (enemyUnit.initialHp);
        }
        yield return new WaitForSeconds (2f);
        if (isDead) {
            state = BattleState.LOST;
            EndBattle ();
        } else {
            state = BattleState.PLAYERTURN;
            PlayerTurn ();
        }
    }
}