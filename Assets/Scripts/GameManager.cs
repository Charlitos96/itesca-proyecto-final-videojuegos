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
    Score score;
    [SerializeField]
    Player player;
    [SerializeField]
    Health health;
    GameData gameData;

    public GameObject TextBox { get => textBox; }
    public GameObject TextoInteractuar { get => textoInteractuar; }
    public Score GetScore { get => score; }
    public Health GetHealth { get => health; }

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

    void OnLevelWasLoaded(int level)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Health = instance.GetHealth.GetHealth;
        
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
        int health = player.Health;
        gameData = new GameData(pos, dir, score, health);
        dataManager.SaveData(gameData);
    }

    public void Load()
    {
        gameData = dataManager.LoadData();
        if(!gameData.Empty)
        {
            player.transform.position = gameData.Position;
            if (gameData.Dir != null)
            {
                player.transform.rotation = gameData.Dir;
            }
            GetScore.SetScore(gameData.Score);
            if(gameData.Health > 0)
            {
                player.Health = gameData.Health;
            }
            //GetHealth.RefreshHealth(player.Health);
        }else{
            gameData.Position = player.transform.position;
        }
    }

    void Update()
    {
        GetScore.Refresh();
        if(player != null)
        {
            GetHealth.RefreshHealth(player.Health);
        }
    }

    public void RestartLevel()
    {
        player.transform.position = gameData.Position;
        GetScore.ResetScore();
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
