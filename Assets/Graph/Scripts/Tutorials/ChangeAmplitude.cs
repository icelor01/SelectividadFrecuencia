using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAmplitude : MonoBehaviour
{
    public InputField InputAmplitude;
    [SerializeField] public GameObject plotComponent;
    // Each slider must have a min, max and an initital value
    [SerializeField] protected int min_value;
    [SerializeField] protected int max_value;
    [SerializeField] protected int initial_value;
    float fill_ini = 1f; //FillAmount value from 0 to 1
    bool wasFocused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            (wasFocused && !InputAmplitude.isFocused))
        {
            //Debug.Log("Return key was pressed.");
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            float amplitude = table.GetAmplitude();
            float amplitude_new = float.Parse(InputAmplitude.text); 
            
            if (amplitude_new != amplitude)
            {
                // Adjust amplitude to fit into its min & max values
                if (amplitude_new > max_value)
                {
                    amplitude_new = max_value;
                    InputAmplitude.text = amplitude_new.ToString();
                }
                else if (amplitude_new < min_value)
                {
                    amplitude_new = min_value;
                    InputAmplitude.text = amplitude_new.ToString();
                }

                table.ChangeAmplitude((int)(System.Math.Round(amplitude_new, 0)));
                table.RequestData(plot);

            }

        }

        wasFocused = InputAmplitude.isFocused;
    }
}
