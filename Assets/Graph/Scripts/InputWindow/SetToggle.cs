//Code extracted and modified from https://www.youtube.com/watch?v=YOaYQrN1oYQ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToggle : MonoBehaviour {

    public bool isSelected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setToggle(bool isSet){
        isSelected = isSet;
    }
}
