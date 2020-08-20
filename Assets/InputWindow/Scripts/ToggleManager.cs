// Code from https://www.youtube.com/watch?v=aCH2J7X_5vM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleManager : MonoBehaviour
{

    [SerializeField] public Toggle toggleYes;
    [SerializeField] public Toggle toggleNo;
    int activeToggleid = 0;

    // Update is called once per frame
    void Update()
    {

    }


    public void ActiveToggle()
    {
        if (toggleYes.isOn)
        {
            activeToggleid = 2;
            Debug.Log("Toggle Yes pressed" + activeToggleid);
        }
       else if (toggleNo.isOn)
        {
            activeToggleid = 1;
            Debug.Log("Toggle No pressed" + activeToggleid);
        }
        else
        {
            activeToggleid = 0;
            Debug.Log("Toggle without being pressed: "+ activeToggleid);
        }
    }

    public int getActiveToogle()
    {
        return activeToggleid;
    }

}
