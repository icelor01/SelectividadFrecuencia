using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSpan : MonoBehaviour
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
        if (System.Math.Round(myTable.GetSpan(),1) == 15.0)
        {
            Escenas.instance.GoToScene(3);
        }
        else
        {
            // Debemos mantenernos en la misma escena
            //do nothing
        }
    }
}
