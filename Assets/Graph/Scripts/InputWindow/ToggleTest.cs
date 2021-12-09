// Code from https://www.youtube.com/watch?v=0b6KmdPcDQU
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleTest : MonoBehaviour {

    ToggleGroup toggleGroupInstance;

    public Toggle CurrentSelection
    {
        get { return toggleGroupInstance.ActiveToggles().FirstOrDefault(); }
    }

	// Use this for initialization
	void Start () {
        //toggleGroupInstance = GetComponent<ToggleGroup>();
        //Debug.Log("First Selected: " + CurrentSelection.name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectToggle (int id)
    {
        var toggles = toggleGroupInstance.GetComponentsInChildren<Toggle>();
        toggles[id].isOn = true;
    }


}
