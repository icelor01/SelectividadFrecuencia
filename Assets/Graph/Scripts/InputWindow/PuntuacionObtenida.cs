using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionObtenida : MonoBehaviour
{
    public Text Texto_Puntuacion;


    // Start is called before the first frame update
    void Awake()
    {
        Texto_Puntuacion.text = "Tu puntuación es de: " + GameManager.manager.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
