using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SpanButtonScript : MonoBehaviour {

    /*
    //Make sure to attach these Buttons in the Inspector
    public Button m_SpanButton;
    private GameObject UI_InputSpan;

    void Start()  {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        m_SpanButton.onClick.AddListener(TaskOnClick);
        m_SpanButton.onClick.AddListener(delegate { TaskWithParameters("Hello"); });
        //m_SpanButton.onClick.AddListener(() => ButtonClicked(42));
        UI_InputSpan = transform.Find("/Canvas/UI_InputSpan").GetComponent<GameObject>();
        //UI_InputSpan = GameObject.Find("/Canvas/UI_InputSpan");
    }

    void TaskOnClick() {
        //Output this to console when SpanButton is clicked
        Debug.Log("You have clicked the button!");
        //UI_InputSpan.SetActive(true);
        UI_InputSpan.GetComponent<Renderer>().enabled = true;
    }

    void TaskWithParameters(string message)  {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
    }

    */
}