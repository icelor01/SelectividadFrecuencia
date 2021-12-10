using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFreq : MonoBehaviour
{
    public InputField InputFreq;
    [SerializeField] public GameObject plotComponent;
    // Each slider must have a min, max and an initital value
    [SerializeField] protected int min_value;
    [SerializeField] protected int max_value;
    [SerializeField] protected float initial_value;
    float fill_ini = 1f; //FillAmount value from 0 to 1
    bool wasFocused = false;

    // Use this for initialization

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || 
            (wasFocused && ! InputFreq.isFocused))
        {
            Debug.Log("Enter / lost focused");
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            float fc = table.Getfc();
            float fc_new = float.Parse(InputFreq.text);

            if (fc_new != fc)
            {
                    // Adjust amplitude to fit into its min & max values
                    if (fc_new > max_value)
                    {
                        fc_new = max_value;
                        InputFreq.text = fc_new.ToString();
                    }
                    else if (fc_new < min_value)
                    {
                        fc_new = min_value;
                        InputFreq.text = fc_new.ToString();
                    }

                table.Changefc(fc_new);
                table.RequestData(plot);
            }

        }

        wasFocused = InputFreq.isFocused;
    }
}