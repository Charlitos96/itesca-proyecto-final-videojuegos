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
    GameObject textoInteractuar;

    public GameObject TextBox { get => textBox; }
    public GameObject TextoInteractuar { get => textoInteractuar; }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
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
