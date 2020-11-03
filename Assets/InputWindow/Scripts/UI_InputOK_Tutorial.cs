using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_InputOK_Tutorial : MonoBehaviour
{

    private static UI_InputOK_Tutorial instance;
    private Button_UI okBtn;
   [SerializeField] public GameObject plotComponent;
    private float fc;
    private int xMin;
    private int xMax;
    private int amplitude;
    //Valores solucion para cada Ejercicio
    public float fc_solution;
    public int xMin_solution;
    public int xMax_solution;
    public int amplitude_solution;
    public Text feedbackText;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
        Hide();
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        //feedbackText = transform.Find("FeedbackText").GetComponent<Text>();
    }

    // Use this for initialization
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            okBtn.ClickFunc();
        }
     }

    private Single relativeError(Single actual, Single expected)
    {
        return System.Math.Abs(actual - expected) / expected;
    }

    private void Show(string inputString, Action onCancel, Action<string> onOk)
    {

        GameManager.manager.tutorial_solutionIsCorrect = false;

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
            errores++;
        }


        // Si la amplitud es incorrecta
        if (ampError > 0.1)
        {
            feedbackText.color = Color.red;
            feedbackText.text = "Revisa las fórmulas y la amplitud de la señal";
            errores++;
        }

       else if (errores == 0)
        {
            Debug.Log("Respuesta correcta");
            feedbackText.color = Color.green;
            feedbackText.text = "¡¡Muy bien!! Has ajustado correctamente el canal";
            GameManager.manager.tutorial_solutionIsCorrect = true;
        }

        Debug.Log("After check " + errores + " errors, from fe= " + freqError + "se=  " + spanError + " ae=" + ampError);
        Debug.Log("Respuesta: Frecuencia: " + fc + " ,span: " + (xMax - xMin) + " amplitud: " + amplitude);
        Debug.Log("Respuesta correcta: Frecuencia: " + fc_solution + " ,span: " + (xMax_solution - xMin_solution) + " amplitud: " + amplitude_solution);

        
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