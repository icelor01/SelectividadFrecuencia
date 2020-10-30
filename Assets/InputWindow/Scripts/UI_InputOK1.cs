using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
//using TMPro;

public class UI_InputOK1 : MonoBehaviour {

    private static UI_InputOK1 instance;
    private Button_UI okBtn;
    //private Button_UI cancelBtn;
    [SerializeField] public GameObject plotComponent;
    private int xMin;
    private int xMax;
    private int amplitude;
    //private int n;
    //Valores solucion para cada Ejercicio
    public int xMin_solution;
    public int xMax_solution;
    public int amplitude_solution;
    public int toggle_solution=1; // No existe selectividad en frecuencia No=1
    public ToggleManager toggleManagerInstance;
    public Text feedbackText;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
        Hide();
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        //feedbackText = transform.Find("FeedbackText").GetComponent<Text>();
        GameManager.manager.solution_isCorrect = false;
    }

    // Use this for initialization
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            okBtn.ClickFunc();
        }
        /* if (Input.GetKeyDown(KeyCode.Escape))
         {
            cancelBtn.ClickFunc();
        }*/

    }


    private void Show(string inputString, Action onCancel, Action<string> onOk)
    {

        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
        xMin = table.Getxmin();
        xMax = table.Getxmax();
        amplitude = table.GetAmplitude();


        // Si no está seleccionado
        if (toggleManagerInstance.activeToggleid == 0)
        {
            Debug.Log("Falta indicar si existe selectividad en frecuencia");
            feedbackText.color = Color.blue;
            feedbackText.text = "Recuerda indicar si existe selectividad en frecuencia o no";

        }

        // Si el span es incorrecto
        else if ( ((xMax - xMin) < (plot.table.GetSpan() - 1)) || ((xMax - xMin) > (plot.table.GetSpan() + 1)) )
        {
            Debug.Log("Span incorrecto. El span debería ser: "+ plot.table.GetSpan());
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y el ancho de banda de la señal";
        }

        // Si la frecuencia es incorrecta
        else if ((((xMax - xMin) / 2) < (plot.table.Getfc() - 1)) || (((xMax-xMin)/2)>(plot.table.Getfc()+1))  )
        {
            Debug.Log("Frecuencia incorrecta. La frecuencia debería ser: "+ plot.table.Getfc());
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y la frecuencia de la señal";
        }
               

        // Si la amplitud es incorrecta
        else if ((amplitude <= amplitude_solution - 1) & (amplitude > amplitude_solution + 1))
        {
            Debug.Log("Amplitud incorrecta");
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y la amplitud de la señal";
        }

        // Si selectividad mal
        else if (toggleManagerInstance.activeToggleid != toggle_solution)
        {
            Debug.Log("Selectividad en frecuencia incorrecto");
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y si existe selectividad en frecuencia o no";
        }


        // Si la solución es correcta
        else if (  ((((xMax - xMin) / 2) > (plot.table.Getfc() + 1)) || (((xMax - xMin) / 2) > (plot.table.Getfc() - 1))) &
           ((xMax - xMin) < (plot.table.GetSpan() - 1)) || ((xMax - xMin) > (plot.table.GetSpan() + 1)) & 
           ((amplitude >= amplitude_solution - 1) & (amplitude <= amplitude_solution + 1))  &
           (toggleManagerInstance.activeToggleid == toggle_solution))
        {
            Debug.Log("Respuesta correcta");
            feedbackText.color = Color.green;
            feedbackText.text = "¡¡Muy bien!! Has ajustado correctamente el canal";
            GameManager.manager.solution_isCorrect = true;


        }
        // Si la solución es incorrecta
        else
        {
            feedbackText.color = Color.red;
            feedbackText.text = "¡Vaya! No has ajustado correctamente el canal. Vuelve a intentarlo o revisa las fórmulas";
        }




        gameObject.SetActive(true);




        okBtn.ClickFunc = () => {
            Hide();
        };

        /*cancelBtn.ClickFunc = () => {
            Hide();
            onCancel();
        };*/

    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }


    public static void Show_Static(string inputString, Action onCancel, Action<string> onOk)
    {
        instance.Show(inputString, onCancel, onOk);

    }



}