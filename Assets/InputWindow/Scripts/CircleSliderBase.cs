// Code extracted and modified from Unity UI Knob Control https://youtu.be/yRKJdUAWn5A
using UnityEngine;
using UnityEngine.UI;

public abstract class CircleSliderBase : MonoBehaviour {

    [SerializeField] Transform handle;
    [SerializeField] Image fill;
    [SerializeField] Text valText;
    Vector3 mousePos;
    [SerializeField] public GameObject plotComponent;
    // Each slider must have a min, max and an initital value
    [SerializeField] int min_value;
    [SerializeField] int max_value;
    [SerializeField] int labelAmount=10;

    [SerializeField] float initial_value;
    private Transform labelTemplateTransform;

    private const float CIRCUMFERENCE_DEGREES = 360;
    private const float ZERO_ANGLE = 260;
    private const float MAX_ANGLE = 280 ;
    private const float ROTATION = 45; //
    float FORBIDDEN_ANGLE = MAX_ANGLE - ZERO_ANGLE;
    float fill_ini = 1f; //FillAmount value from 0 to 1



    private void Awake() {
         /*    
        labelTemplateTransform = transform.Find("labelTemplate");
        labelTemplateTransform.gameObject.SetActive(false);
        CreateLabels();
         
        //Valor inicial manilla
        Vector2 dir = handle.position;
        Debug.Log("Positición manilla:" + dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log("Ángulo inicial:" + angle);
        float initial_rotation = (CIRCUMFERENCE_DEGREES * initial_value) / (max_value - min_value);
        Debug.Log("Rotación inicial:" + initial_rotation);
        Quaternion r = Quaternion.AngleAxis(angle-ROTATION+initial_rotation, Vector3.forward);
        handle.rotation = r;
        */
        
     
    }

   
    public void onHandleDrag() {
        // Leemos la posición actual del dial
         mousePos = Input.mousePosition;
        //Comparamos la posición actual con la inicial
        Vector2 dir = mousePos - handle.position;
        // Calculamos el incremento de ángulo
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log("Ángulo:" +angle);
        
        angle = (angle <= 0) ? (angle + CIRCUMFERENCE_DEGREES) : angle; // módulo del ángulo: si el ángulo es <=0, suma 360º, si no, toma el valor del ángulo 
        //Debug.Log("Módulo del ángulo:" + angle);
        //Angle within the allowed zone
        if (angle <= ZERO_ANGLE || angle >= MAX_ANGLE)   {
            
           Quaternion r = Quaternion.AngleAxis(angle-ROTATION, Vector3.forward); //
           handle.rotation = r;

            // La siguiente línea solo oculta valores
            //angle = ((angle >= MAX_ANGLE) ? (angle - CIRCUMFERENCE_DEGREES) : angle) +FORBIDDEN_ANGLE; //si el ángulo es >=ZERO_ANGLE, resta 360º, si no, toma el valor del ángulo 


            // Assigns the fillAmount value: 
            //fill.fillAmount = 0.75f - (angle / CIRCUMFERENCE_DEGREES);
            fill.fillAmount =fill_ini -(angle/ CIRCUMFERENCE_DEGREES);
            float value= (fill.fillAmount * (max_value-min_value));
            // Assigns the fillAmount value to text
            if (min_value<0) {
                valText.text = (value-max_value).ToString();
            }
            else {
                valText.text = (value).ToString();
            }
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();

            if (min_value < 0)
            {
                UpdatePlot(plot, table, (value - max_value));
            }
            else
            {
                UpdatePlot(plot, table, value);
            }

           
        }

    }

    private void CreateLabels() {

     float totalAngleSize = ZERO_ANGLE + (CIRCUMFERENCE_DEGREES- MAX_ANGLE);
    

        for (int i=0; i<=labelAmount; i++) {
            Transform labelTransform =Instantiate(labelTemplateTransform, transform);
            float labelNormalized = (float)i / labelAmount;
            float labelAngle = ZERO_ANGLE - labelNormalized  * totalAngleSize;
            labelTransform.eulerAngles = new Vector3(0, 0, labelAngle);
            if (min_value < 0)
            {
                labelTransform.Find("labelText").GetComponent<Text>().text = Mathf.RoundToInt((labelNormalized * (max_value - min_value)) - max_value).ToString();
            }
            else
            {
                labelTransform.Find("labelText").GetComponent<Text>().text = Mathf.RoundToInt((labelNormalized * (max_value - min_value))).ToString();
            }

            labelTransform.Find("labelText").eulerAngles = Vector3.zero;
            labelTransform.gameObject.SetActive(true);

        }



    }



    public abstract void UpdatePlot(Plot plot, Table table, float value);

}
