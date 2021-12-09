using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Tooltip_old : MonoBehaviour
{
    private static Tooltip_old tooltip;

    private Camera uiCamera;
    [SerializeField] private Text tooltipText;
    [SerializeField] private RectTransform backgroundRectTransform;

    public void Awake() {
        //backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        //tooltipText=transform.Find("text").GetComponent<Text>();
               
        tooltip = this;
        //tooltip.gameObject.SetActive(false);
        /*
        ShowTooltip("Random tooltip text");

        
        FunctionPeriodic.Create( () => {
            string abc = "hola hola +\ngpj";
            string showText = "";
            for (int i = 0; i < Random.Range(30, 150); i++) { 
                showText += abc[Random.Range(0, abc.Length)];
            }
            ShowTooltip(showText);
        }, .5f);
        */
    }

    public void Update() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

       
   private void ShowTooltip (string tooltipString) {
        tooltip.gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize*2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }


    private void HideTooltip() {
        tooltip.gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        tooltip.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        tooltip.HideTooltip();
    }

}
