using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour {

    public Slider sliderInstance;

	public void OnValueChanged (float value) {
        Debug.Log ("New value: " + value);
		
	}
}
