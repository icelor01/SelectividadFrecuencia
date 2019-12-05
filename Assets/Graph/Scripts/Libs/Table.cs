using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

/*
 * TableLib: Librería TableAdventure para representar una colección de puntos * 
 * conjunto de puntos en coma flotante, con valores entre 0 y 1
  */


public class Table : MonoBehaviour {

    public int numRows;
    public int numCols;
   
    public float[,] tableArray;


    // Use this for initialization
    void Start () {

      }
	
    
	// Update is called once per frame
	void Update () {
		
	}

    void rellena() {

        tableArray = new float[numCols, numRows];
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)  {
                // tableArray[i, j] = valor;
            }
        }
    }


    

  }


