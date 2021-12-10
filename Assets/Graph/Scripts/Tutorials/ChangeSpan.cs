using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpan : MonoBehaviour
{
    public InputField InputSpan;
    [SerializeField] public GameObject plotComponent;
    // Each slider must have a min, max and an initital value
    [SerializeField] protected int min_value;
    [SerializeField] protected int max_value;
    [SerializeField] protected float initial_value;
    float fill_ini = 1f; //FillAmount value from 0 to 1
    bool wasFocused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            (wasFocused && !InputSpan.isFocused))
        {
       
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();

            float span = table.GetSpan();
            float span_new = float.Parse(InputSpan.text);

            if (span_new != span)
            {
                // Adjust amplitude to fit into its min & max values
                if (span_new > max_value)
                {
                    span_new = max_value;
                    InputSpan.text = span_new.ToString();
                }
                else if (span_new < min_value)
                {
                    span_new = min_value;
                    InputSpan.text = span_new.ToString();
                }

                table.ChangeSpan(span_new);
                table.RequestData(plot);

            } 

        }

        wasFocused = InputSpan.isFocused;
    }
}
