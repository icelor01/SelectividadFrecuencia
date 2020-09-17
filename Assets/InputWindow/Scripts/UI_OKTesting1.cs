using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class UI_OKTesting1 : MonoBehaviour
{


    private void Start()
    {

        transform.Find("OKButton").GetComponent<Button_UI>().ClickFunc = () => {
            //inputSpan.Show(() => {
            UI_InputOK1.Show_Static("", () => {
                // Clicked Cancel
                CMDebug.TextPopupMouse("Cancel!");
            }, (string inputText) => {
                // Clicked Ok
                CMDebug.TextPopupMouse("Ok: " + inputText);

            });
        };

    }
}
