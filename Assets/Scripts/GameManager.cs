using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Core.Movement;

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
    [SerializeField]
    Score score;
    GameData gameData;

    public GameObject TextBox { get => textBox; }
    public GameObject TextoInteractuar { get => textoInteractuar; }
    public Score GetScore { get => score; }

    void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        Load();
    }
    public void Save()
    {
        Vector3 pos = player.transform.position;
        Quaternion dir = player.transform.rotation;
        int score = GetScore.GetScore;
        gameData = new GameData(pos, dir, score);
        dataManager.SaveData(gameData);
    }

    public void Load()
    {
        gameData = dataManager.LoadData();
        player.transform.position = gameData.Position;
        if (gameData.Dir != null)
        {
            player.transform.rotation = gameData.Dir;
        }
        GetScore.SetScore(gameData.Score);
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
