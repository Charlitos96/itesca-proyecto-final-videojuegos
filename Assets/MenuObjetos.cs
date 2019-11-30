using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuObjetos : MonoBehaviour
{
     public GameObject flecha2,lista2;
     int indice = 0;
     bool active;
     Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
       Dibujar();

    }

    // Update is called once per frame
    void Update()
    {   
          if(Input.GetKeyDown("space"));  
          active = !active;      
          //if(Input.GetKeyDown("space"));
        bool up = Input.GetKeyDown("up");
        bool down = Input.GetKeyDown("down");
        
        //if(Input.GetKeyDown("return")) Accion();
        
        if (up) indice--;
        if (down) indice++;

        if(indice > lista2.transform.childCount-1) indice = 0;
        else if(indice < 0)indice = lista2.transform.childCount-1;

        if(up || down) Dibujar();

        if(Input.GetKeyDown("return")) Accion();

    } 
    void Dibujar(){
        Transform opcion = lista2.transform.GetChild(indice);
        flecha2.transform.position = opcion.position;  
    }
     void Accion(){
        
        Transform opcion = lista2.transform.GetChild(indice);
        if(opcion.gameObject.name == "Salir"){
             print("cerrando juego...");
             Application.Quit();

         }else{
             
             
         SceneManager.LoadScene(opcion.gameObject.name);
         }
     }
}
    
