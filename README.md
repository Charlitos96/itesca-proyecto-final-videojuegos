**COMBATE**

 
**INTERFAZ DE LA BATALLA**
 
`

public class BattleHUD : MonoBehaviour {
    public Text txtName;
    public Slider hpSlider;

    public void SetHUD (Unit unit) {
        txtName.text = unit.unitName;
        hpSlider.maxValue = unit.hp;
        hpSlider.value = unit.initialHp;
    }

    public void setHP (int hp) {
        hpSlider.value = hp;
    }
}`

Se crea la clase para inicializar los datos que tendra el jugador durante una batalla.

**BATALLA**

`public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

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
`
Se definen y crean las instancias a necesitar: state (Tipo BattleState, crea la instancia del enum, que va a definir en qué estado se encuentra la batalla), playerPrefab (Tipo GameObject, prefab que manejará la batalla y representará el jugador. Contiene el modelo y su script Unit correspondiente), enemyPrefab (Tipo GameObject, cumple con la misma función que palyerPrefab pero correspondiente al enemigo), playerTile (Tipo Transform, elementos Transform de la plataforma en donde aparece el jugador, el cual calcula su posición automáticamente), enemyTile (Tipo Transform, cumple con la misma función de playerTile pero correspondiente al enemigo), playerUnit (Tipo Unit, es la referencia al script Unit que contiene el jugador), enemyUnit (Tipo Unit, es la referencia al script Unit que contiene el enemigo), textoNombre (Tipo Text, texto para el panel izquierdo de la UI), textoDialogo (Tipo Text, es el texto para el panel de diálogo de la descripción), playerHUD (Tipo BattleHUD, el UI correspondiente al jugador) y enemyHUD (Tipo BattleHUD, el UI correspondiente al enemigo)

`void Start () {
        state = BattleState.START; //Se inicializa el estado en START
        StartCoroutine (SetupBattle ()); //Se crea una corutina (Una especie de hilo), que inicializará la batalla
    }`
    
 Start() es en donde se inicializa el estado de la batalla con StartCorouting(SetupBattle()); en donde se crea una corutina que inicializará la batalla.
 `
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
    }`
    
Dentro de SetupBattle se lleva a cabo el script necesario para construir el escenario de batalla y preparar a los “personajes”. Se extraen los componentes de los personajes, se inicializan los cuadros de dialogo y los HUDs correspondientes para así, comenzar con el turno del jugador dentro de la batalla, llamando a PlayerTurn().

`
void PlayerTurn () {
        playerUnit.Descubrirse (); //Cada que inicia este método significa que se acaba de cambiar a este estado por lo que es necesario hacer que el jugador ya no se proteja más en caso de que lo estuviera haciendo.
        textoDialogo.text = "Escoge una acción..."; //Se le dice al usuario que escoga una acción.
    }`
 Este método representa el turno del jugador para seleccionar una acción dentro de la batalla. Cada vez que se inicia el método playerUnit.Descubirse() con un llamado, significa que el estado anterior (en caso de no ser el primer turno) de “protegerse” ya no es válido y vuelve a su estado original. Dentro de textoDialogo.text se indica al jugador (en este caso) que debe de seleccionar su acción. 

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
    }`
    
 El método onAttackBtn() es accionado cuando se presiona el botón de “Ataque”. Este método puede tomar dos caminos: si se encuentra en el turno del enemigo (no se cumple con la condición BattleState.PLAYERTURN) comenzará la corutina Atacar(), por el contrario, si la condición se cumple entrará a un return. onDefendBtn() y onHealyBtn() funcionan de manera similar a  onAttackBtn().
 
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

Curar() y Defender() funcionan de manera similar. En ambos métodos aparece un texto indicando su acción correspondiente, ya sea haber seleccionado curar (“Te has curado”) o defender (“Te has defendido”). Ambos llaman al método correspondiente a su acción (playerUnit.Curarse(20) o playerUnit.Cubrirse()) con la diferencia de que playerUnit.Curarse(20) tiene un parámetro que indica la cantidad de vida a restaurar al personaje. Curar() después llama a una actualización de HUD de batalla con playerHUD.setHP(playerUnit.initialHp). Ambos métodos tienen una pausa para ayudar al jugador a entender lo que ha pasado y después cambia el estado al turno del enemigo, iniciando con la corutina del enemigo dentro de SartCoroutine(EnemyTurn()); 

**METODO ATACAR**

`
IEnumerator Atacar () {
        bool isDead = false; 
        System.Random rnd = new System.Random (); 
        int n = rnd.Next (1, 101); 
        if (n >= 90) { 
            textoDialogo.text = "Ataque fallido...";
        } else {
            isDead = enemyUnit.recibirDmg (playerUnit.dmg); 
            enemyHUD.setHP (enemyUnit.initialHp); 
            if (enemyUnit.defended) { 
                textoDialogo.text = "¡Ataque defendido!";
            } else {
                textoDialogo.text = "¡Ataque realizado con éxito!";
            }
        }
        yield return new WaitForSeconds (2f); 
        if (isDead) { 
            state = BattleState.WON;
            EndBattle ();
        } else { 
            state = BattleState.ENEMYTURN;
            StartCoroutine (EnemyTurn ());
        }
    }`
    
  Cuando el jugador esta a punto de atacar se debe verificar el estado de salud del enemigo, durante el ataque se verifica si el enemigo se cubrio para determinar la cantidad de energia que el enemigo va perder.  Si el enemigo sigue con vida despues del ataque entonces su turno comienza para atacar al jugador.
  
  **TERMINAR BATALLA**
  
  `void EndBattle () {
        playerUnit.Descubrirse (); //Se descubren los jugadores por seguridad
        enemyUnit.Descubrirse ();
        if (state == BattleState.WON) { //Si el estado es WON entonces se imprime que se ganó la batalla, de otra forma se imprime que se perdió
            textoDialogo.text = "¡GANASTE LA BATALLA!";
        } else {
            textoDialogo.text = "Perdiste la batalla...";
        }
    }`
    
Se verifica si el jugador o el enemigo fueron derrotados, en caso de que el enemigo sea derrotado antes que el jugador entonces el metodo avisa que el jugador gano la batalla, o en caso contrario si el enemigo fue el vencedor entonces el metodo avisa que el jugador perdio la batalla.
    
    
  **TURNO DEL ENEMIGO**
  
  `IEnumerator EnemyTurn () {
        enemyUnit.Descubrirse ();
        bool isDead = false;
        textoDialogo.text = "Turno de " + enemyUnit.unitName;
        yield return new WaitForSeconds (2f);
        System.Random rnd = new System.Random ();
        int n = rnd.Next (1, 151); 
        if (n <= 100) {
            //Ataca
            int n2 = rnd.Next (1, 101);
            if (n2 <= 90) {
                isDead = playerUnit.recibirDmg (enemyUnit.dmg);
                if (playerUnit.defended) {
                    textoDialogo.text = "¡Ataque defendido!";
                } else {
                    textoDialogo.text = "!" + enemyUnit.unitName + " ataca!";
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
}`

Si el jugador despues de haber efectuado un ataque, el enemigo sigue vivo entonces se ejecuta el metodo para turno del enemigo el cual puede efectuar un ataque contra el jugador o tambien curarse puntos de vida que el jugador le haya restado en un ataque, si el enemigo ataca el jugador tendra la oportunidad de defenderse y no recibir mucho daño del enemigo pero si el enemigo efectua bien el ataque y el jugador no se defiende, la batalla puede terminar a favor del enemigo. Si el enemigo decide solo curarse entonces su turno termina.
    
    
    
    
    
    
    
    
    
