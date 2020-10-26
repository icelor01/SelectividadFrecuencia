using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public Button CustomButton;

    void Awake() {
        //  CustomButton.onClick.AddListener(delegate { CustomButton_OnClick(); });
        CustomButton.onClick.AddListener(CustomButton_OnClick);
    }

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//Handle the onClick event
    void CustomButton_OnClick() {
    
    }

}
