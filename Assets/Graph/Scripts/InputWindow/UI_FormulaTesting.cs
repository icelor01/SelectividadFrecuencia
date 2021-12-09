using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class UI_FormulaTesting : MonoBehaviour {

    private void Start()
    {
        //transform.Find("m_SpanButton").GetComponent<Button_UI>().ClickFunc=inputSpan.Show;
        transform.Find("FormulaButton").GetComponent<Button_UI>().ClickFunc = () => {
            //inputSpan.Show(() => {
            UI_InputFormula.Show_Static("", () => {
                // Clicked Cancel
                CMDebug.TextPopupMouse("Cancel!");
            }, (string inputText) => {
                // Clicked Ok
                CMDebug.TextPopupMouse("Ok: " + inputText);

            });
        };


    }

}
