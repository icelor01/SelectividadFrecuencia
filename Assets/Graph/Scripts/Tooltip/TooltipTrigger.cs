using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string content;

    public void OnPointerEnter (PointerEventData eventData)
    {
        TooltipSystem.Show(content);        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide(); 
    }
       
}
