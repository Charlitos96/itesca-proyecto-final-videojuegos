using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject textBox;
    [SerializeField]
    Dialogo dialogo;
    [SerializeField]
    DataManager dataManager;
    [SerializeField]
    GameObject textoInteractuar;
    [SerializeField]
    Player player;
    GameData gameData;

    public GameObject TextBox { get => textBox; }
    public GameObject TextoInteractuar { get => textoInteractuar; }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        gameData = dataManager.LoadData();
        Debug.Log($"Position: {gameData.Position}");
        player.transform.position = gameData.Position;
        /*Debug.Log($"posX: {gameData.PosX}");
        Debug.Log($"posY: {gameData.PosY}");
        player.transform.position = new Vector2(gameData.PosX,gameData.PosY);*/
    }
    public void Save()
    {
        /*Vector2 pos = player.transform.position;
        gameData = new GameData(pos.x, pos.y);*/
        Vector3 pos = player.transform.position;
        gameData = new GameData(pos);
        dataManager.SaveData(gameData);
    }

    void Update()
    {
        
    }

    public void MostrarTexto(string text)
    {
        TextBox.SetActive(true);
        dialogo.setDialog(text);
    }
    public void OcultarTexto()
    {
        TextBox.SetActive(false);
    }

}
