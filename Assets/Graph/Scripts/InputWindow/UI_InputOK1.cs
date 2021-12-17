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
    private float fc;
    private float xMin;
    private float xMax;
    private float amplitude;
    //private int n;
    //Valores solucion para cada Ejercicio
    public float fc_solution;
    public float xMin_solution;
    public float xMax_solution;
    public float amplitude_solution;
    public float toggle_solution=1; // No existe selectividad en frecuencia No=1
    public ToggleManager toggleManagerInstance;
    public Text feedbackText;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
        Hide();
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
    }

    // Use this for initialization
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            okBtn.ClickFunc();
        }
    }

    private Single relativeError(Single actual, Single expected) {
        return System.Math.Abs(actual - expected) / expected;
    }


    private void Show(string inputString, Action onCancel, Action<string> onOk)
    {

        GameManager.manager.solutionIsCorrect = false;

        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
        fc = table.Getfc();
        xMin = table.Getxmin();
        xMax = table.Getxmax();
        amplitude = table.GetAmplitude();

        //Single freqError = relativeError((xMax - xMin) / 2, table.Getfc());
        //Single spanError = relativeError(xMax - xMin, table.GetSpan());
        Single freqError = relativeError(fc, fc_solution);
        Single spanError = relativeError((xMax - xMin), (xMax_solution - xMin_solution));
        Single ampError = relativeError(amplitude, amplitude_solution);
        int errores = 0;

        // Si la frecuencia es incorrecta
        if (freqError > 0.1)
        {
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y la frecuencia de la señal";
            Debug.Log("La frecuencia marcada es: ");
            errores++;
        }

        // Si el span es incorrecto
        if (spanError > 0.1)
        {
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y el ancho de banda de la señal";
            errores ++;
        }
              

        // Si la amplitud es incorrecta
        if (ampError > 0.1)
        {
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y la amplitud de la señal";
            errores ++;
        }

        // Si selectividad mal
        if (toggleManagerInstance.activeToggleid == 0)
        {
            Debug.Log("Falta indicar si existe selectividad en frecuencia");
            feedbackText.color = Color.blue;
            feedbackText.text = "Recuerda indicar si existe selectividad en frecuencia o no";
            errores ++;
        }
        else if (toggleManagerInstance.activeToggleid != toggle_solution)
        {
            Debug.Log("Selectividad en frecuencia incorrecto");
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y si existe selectividad en frecuencia o no";
            errores ++;
        }

        if (errores == 0) {
            Debug.Log("Respuesta correcta");
            feedbackText.color = Color.green;
            feedbackText.text = "¡¡Muy bien!! Has ajustado correctamente el canal";
            GameManager.manager.solutionIsCorrect = true;
        } 

        String response = "After check " + errores + " errors, from fe= " + freqError + "se=  " + spanError + " ae=" + ampError;
        Debug.Log(response);
        Debug.Log("Respuesta: Frecuencia: " + fc + " ,span: " + (xMax - xMin) + " amplitud: " + amplitude);
        Debug.Log("Respuesta correcta: Frecuencia: " + fc_solution + " ,span: " + (xMax_solution - xMin_solution) + " amplitud: " + amplitude_solution);

        GameManager.manager.TrackAttempt(response);
        
        gameObject.SetActive(true);

        okBtn.ClickFunc = () => Hide();
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