using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckFrecuency : MonoBehaviour {

    public Plot myChannel;
    public Table myTable;


    // Use this for initialization
    void Awake () {
        myTable = myChannel.getTable();
    }
	
	// Update is called once per frame
	void Update () {
        myTable = myChannel.getTable();
        if (System.Math.Round(myTable.Getfc(), 1) == 10.0) {
            Escenas.instance.GoToScene(2);
        }
        else
        {
            // Debemos mantenernos en la misma escena
            //do nothing
        }
    }
}
