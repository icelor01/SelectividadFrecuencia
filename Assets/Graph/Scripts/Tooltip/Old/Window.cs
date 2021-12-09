using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Window : MonoBehaviour
{

    [SerializeField] private Button_UI InputFreq;

    // Start is called before the first frame update

    /*
    private void Awake()
    {
        Tooltip.HideTooltip_Static();
    }
    */

        private void Start()
    {
        InputFreq.GetComponent<Button_UI>().MouseOverOnceFunc= () => Tooltip_old.ShowTooltip_Static("La frecuencia mínima es 0 y la máxima 500");
        InputFreq.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip_old.HideTooltip_Static();
    }

}
