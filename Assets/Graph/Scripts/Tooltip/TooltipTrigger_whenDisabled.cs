using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTrigger_whenDisabled : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string content;
    public Button playButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!playButton.interactable)
        { TooltipSystem.Show(content); }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

}