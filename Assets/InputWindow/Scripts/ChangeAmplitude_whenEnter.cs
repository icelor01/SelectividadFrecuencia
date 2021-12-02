using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAmplitude_whenEnter : MonoBehaviour
{
    public InputField InputAmplitude;
    [SerializeField] public GameObject plotComponent;
    // Each slider must have a min, max and an initital value
    [SerializeField] protected int min_value;
    [SerializeField] protected int max_value;
    [SerializeField] protected float initial_value;
    float fill_ini = 1f; //FillAmount value from 0 to 1

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Return key was pressed.");
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            int amplitude = table.GetAmplitude();
            int amplitude_new = int.Parse(InputAmplitude.text); ;
            //table.PlotGraphFromInterval(plot, xMin, xMax);
            if (amplitude_new != amplitude)
            {
                table.ChangeAmplitude(amplitude_new);
                table.RequestData(plot);

            }
        }
    }
}
