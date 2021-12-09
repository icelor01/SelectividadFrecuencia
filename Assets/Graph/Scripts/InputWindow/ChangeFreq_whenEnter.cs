using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFreq_whenEnter : MonoBehaviour
{
    public InputField InputFreq;
    [SerializeField] public GameObject plotComponent;
    // Each slider must have a min, max and an initital value
    [SerializeField] protected int min_value;
    [SerializeField] protected int max_value;
    [SerializeField] protected float initial_value;
    float fill_ini = 1f; //FillAmount value from 0 to 1

    // Use this for initialization

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Return key was pressed.");

            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            float fc = table.Getfc();
            // fc inicial=0 El dial fc suma el valor indicado de frecuencia

            float fc_new = float.Parse(InputFreq.text);

            if (fc_new != fc)
            {
                table.Changefc(fc_new);
                table.RequestData(plot);
            }

        }

    }
}