using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeFrequency : MonoBehaviour {

    public Button Button;

    // Use this for initialization
    void Start () {
        Debug.Log("The button is awake");

        //btn1 = GetComponent<Button>();
        //Calls the TaskOnClick/TaskWithParameters method when you click the Button
        Button.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
    }

    void TaskOnClick()    {
        //Output this to console when the Button is clicked
        Debug.Log("You have clicked the button!");
        //GameObject.Find("ElDeXF").GetComponent<Plot_Xf>().repaint();
    }

    void TaskWithParameters(string message)    {
        //Output this to console when the Button is clicked
        Debug.Log(message);
    }


   /*
    * 
    private void OnMouseUpAsButton()    {
        StartCoroutine(GameManager.instance.plotter.GetText("http://localhost:8000/cosa2.txt"));
    }
    */


}
