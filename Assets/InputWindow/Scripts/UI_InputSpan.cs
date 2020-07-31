using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
//using TMPro;

public class UI_InputSpan : MonoBehaviour {

    
    private static UI_InputSpan instance;
    private Button_UI okBtn;
    private Button_UI cancelBtn;
    private Text xMin_text;
    private Text xMax_text;
    private Text n_text;
    private InputField InputXMin;
    private InputField InputXMax;
    private InputField Input_n;
    [SerializeField] public GameObject plotComponent;

    // Use this for initialization
    private void Awake() {
        
        instance = this;
        
        Hide();
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        cancelBtn = transform.Find("cancelBtn").GetComponent<Button_UI>();
        //xMin_text = transform.Find("xMin_text").GetComponent<Text>();
        //xMax_text = transform.Find("xMax_text").GetComponent<Text>();
        //Input_XMin = transform.Find("Input_XMin").GetComponent<InputField>();
        //Input_XMax = transform.Find("Input_XMax").GetComponent<InputField>();
        InputField InputXMin = GameObject.Find("Input_XMin").GetComponent<InputField>();
        InputField InputXMax = GameObject.Find("Input_XMax").GetComponent<InputField>();
        InputField Input_n = GameObject.Find("Input_n").GetComponent<InputField>();

    }

    private void Update()  {
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            okBtn.ClickFunc();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            cancelBtn.ClickFunc();
        }
        
    }


    //public void Show(string titleString, string inputString) {
    //public void Show(string validCharacters, Action onCancel, Action<string> onOk)    {
    private void Show(string inputString, Action onCancel, Action<string> onOk)   {
            gameObject.SetActive(true);
       // InputXMin.text = inputString;
        /*
        transform.SetAsLastSibling();
        xMin_text.text = titleString;
              
        Input_XMin.onValidateInput = (string text, int charIndex, char addedChar) => {
            return ValidateChar(validCharacters, addedChar);
        };
         */

        //Input_XMin.Select();

        
        //String ObjectsText = txt_Input.text;
        int characterLimit = 3;
        InputXMin = GameObject.Find("Input_XMin").GetComponent<InputField>();
        InputXMax = GameObject.Find("Input_XMax").GetComponent<InputField>();
        Input_n = GameObject.Find("Input_n").GetComponent<InputField>();
        InputXMin.characterLimit = characterLimit;
        InputXMax.characterLimit = characterLimit;
        InputField.ContentType IntegerNumber = default(InputField.ContentType);
        InputXMin.contentType= IntegerNumber;
        InputXMax.contentType = IntegerNumber;
        Input_n.contentType = IntegerNumber;

        okBtn.ClickFunc = () => {
            InputField InputXMin = GameObject.Find("Input_XMin").GetComponent<InputField>();
            InputField InputXMax = GameObject.Find("Input_XMax").GetComponent<InputField>();
            InputField Input_n = GameObject.Find("Input_n").GetComponent<InputField>();
            Debug.Log("XMin: " + InputXMin.text);
            Debug.Log("XMax: " + InputXMax.text);
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            int xMin = int.Parse(InputXMin.text);
            int xMax = int.Parse(InputXMax.text);
            int n = int.Parse(Input_n.text);
            //table.PlotGraphFromInterval(plot, xMin, xMax);
            table.ChangeUrl(xMin, xMax, n);
            table.RequestData(plot);
            onOk(InputXMin.text);
            Hide();
        };

        cancelBtn.ClickFunc = () => {
            Hide();
            onCancel();
        };
       
    }

    public void Hide()  {
        gameObject.SetActive(false);
    }

    // public delegate char OnValidateInput(string text, int charIndex, char addedChar); //Dentro de TextMeshPro

    /*
    private char ValidateChar(string validCharacters, char addedChar) {
        if (validCharacters.IndexOf(addedChar) != -1)  {
            // Valid
            return addedChar;
        }
        else  {
            // Invalid
            return '\0';
        }
    }
    
 
    public static void Show_Static(string titleString, string inputString, string validCharacters, int characterLimit, Action onCancel, Action<string> onOk)
    {
        instance.Show(titleString, inputString, validCharacters, characterLimit, onCancel, onOk);
    }

    public static void Show_Static(string titleString, int defaultInt, Action onCancel, Action<int> onOk)
    {
        
        instance.Show(titleString, defaultInt.ToString(), "0123456789-", 20, onCancel,
            (string inputText) => {
                // Try to Parse input string
                if (int.TryParse(inputText, out int _i)) {
                    onOk(_i);
                }
                else  {
                    onOk(defaultInt);
                }
            }
        );
        
    } 
*/

    public static void Show_Static(string inputString, Action onCancel, Action<string> onOk)  {
        instance.Show(inputString, onCancel, onOk);

        /*
        instance.Show(onCancel,
            (string inputText) => {
                // Try to Parse input string
                if (int.TryParse(inputText, out int i))
                {
                    onOk(_i);
                }
                else
                {
                    onOk(defaultInt);
                }
            }
        );
        */

    }



}
