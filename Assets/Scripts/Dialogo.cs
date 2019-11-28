using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Dialogo : MonoBehaviour
{
    //[SerializeField, Range(0.01f, 0.1f)]
    float textSpeed = 0.05f;
    Text texto;
    bool talking;
    string text;
    void Awake()
    {
        texto = GetComponent<Text>();
    }

    public void setDialog(string text)
    {
        this.text = text;
        StartCoroutine("StartDialog");
    }

    IEnumerator StartDialog()
    {
        talking = false;
        string dialog = "";
        talking = true;
        for(int i =0; i< text.Length && talking == true; i++)
        {
            dialog += text[i];
            texto.text = dialog;
            yield return new WaitForSeconds(textSpeed);
        }
    }


}
