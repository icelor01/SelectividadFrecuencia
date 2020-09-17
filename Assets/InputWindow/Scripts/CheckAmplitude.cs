using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAmplitude : MonoBehaviour
{

    public Plot myChannel;
    public Table myTable;


    // Use this for initialization
    void Awake()
    {
        myTable = myChannel.getTable();
    }

    // Update is called once per frame
    void Update()
    {
        if (myTable.GetAmplitude()== 4.0)
        {
            Escenas.instance.GoToScene(4);
        }
        else
        {
            // Debemos mantenernos en la misma escena
            //do nothing
        }
    }
}
