using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase estática para acceder desde cualquier escena

public class GameManager : MonoBehaviour { 

    public static GameManager instance; //instancia de GameManeger
   // public Plot_Graph_from_txtfile_Server plotter; 

    private void Awake()    {
        print("Awake GameManager");
        instance = this;
    }

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update()  {

    }
}
