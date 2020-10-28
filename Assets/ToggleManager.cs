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
    // Active Toggle: Without being pressed=0, No=1, Yes=2
    public int activeToggleid = 0;

    void Awake() {
        toggleYes.onValueChanged.AddListener(delegate { ReportUpdate(toggleYes.isOn, false); });
        toggleNo.onValueChanged.AddListener(delegate { ReportUpdate(false, toggleNo.isOn); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Active Toggle: Without being pressed=0, No=1, Yes=2
    public void ReportUpdate(bool yes, bool no)
    {
        if (yes)
        {
            activeToggleid = 2;
            Debug.Log("Toggle Yes pressed" + activeToggleid);
        }
        else if (no)
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
