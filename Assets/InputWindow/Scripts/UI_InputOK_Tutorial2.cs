using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_InputOK_Tutorial2 : MonoBehaviour
{
    private static UI_InputOK_Tutorial2 instance;
    private Button_UI okBtn;
   
    //Valores solucion para cada Ejercicio
    public ToggleManager toggleManagerInstance;
    public int toggle_solution = 2; // No existe selectividad en frecuencia =1, Sí existe selectividad en frecuencia =2
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


    private void Show(string inputString, Action onCancel, Action<string> onOk)
    {

        // Si no está seleccionado
        if (toggleManagerInstance.activeToggleid == 0)
        {
            Debug.Log("Falta indicar si existe selectividad en frecuencia");
            feedbackText.color = Color.blue;
            feedbackText.text = "Recuerda indicar si existe selectividad en frecuencia o no";

        }

        // Si la solución es correcta
        else if (toggleManagerInstance.activeToggleid == toggle_solution)
        {
            Debug.Log("Respuesta correcta");
            feedbackText.color = Color.green;
            feedbackText.text = "¡¡Muy bien!! Efectivamente, el canal es selectivo en frecuencia y su respuesta en frecuencia no es plana durante el ancho de banda del radiocanal";
            GameManager.manager.solutionIsCorrect = true;


        }
        // Si la solución es incorrecta
        else
        {
            feedbackText.color = Color.red;
            feedbackText.text = "¡Vaya! Ten en cuenta que la forma de onda de la entrada, se modifica a la salida y que Bw=14 kHz y Bc=4kHz. Revisa las fórmulas y vuelve a intentarlo";
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