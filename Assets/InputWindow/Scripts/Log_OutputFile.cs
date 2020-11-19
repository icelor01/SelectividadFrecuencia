using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log_OutputFile : MonoBehaviour
{
  
    public Text Texto_OutputFile;  
    
      void Start()
    {
    Texto_OutputFile.text = "Recuerda entregar en el buzón de la actividad el fichero de juego, con nombre: " + GameManager.manager.outputFile_name +" ubicado en el directorio: " + GameManager.manager.outputFile;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
