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
    [SerializeField] protected int min_value;
    [SerializeField] protected int max_value;
    [SerializeField] protected float initial_value;
    private Transform labelTemplateTransform;

    private const float CIRCUMFERENCE_DEGREES = 360;
    float fill_ini = 1f; //FillAmount value from 0 to 1

    public void Awake() {
        FirstUpdate();
    }

    protected void FirstUpdate() {
         
        //Valor inicial manilla
        Vector2 dir = handle.position;
        //Debug.Log("Positición manilla:" + dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log("Ángulo inicial:" + angle);
        float initial_rotation = (CIRCUMFERENCE_DEGREES * initial_value) / (max_value - min_value);
        //Debug.Log("Rotación inicial:" + initial_rotation);
        Quaternion r = Quaternion.AngleAxis(angle + initial_rotation, Vector3.forward);
        handle.rotation = r;

        valText.text = (initial_value).ToString();
    }
   
    public void onHandleDrag() {
        
        // Leemos la posición actual del dial
        mousePos = Input.mousePosition;
        
        //Comparamos la posición actual con la inicial
        Vector2 dir = mousePos - handle.position;
        
        // Calculamos el incremento de ángulo
        // módulo la circunferencia: si el ángulo es <=0, suma 360º, si no, toma el valor del ángulo         
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (angle + CIRCUMFERENCE_DEGREES) : angle; 
        //Debug.Log("Módulo del ángulo:" + angle);
        //Angle within the allowed zone
            
        Quaternion r = Quaternion.AngleAxis(angle, Vector3.forward); //
        handle.rotation = r;

        // Assigns the fillAmount value: 
        fill.fillAmount = fill_ini - (angle / CIRCUMFERENCE_DEGREES);
        float value= (fill.fillAmount * (max_value - min_value));
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

    public abstract void UpdatePlot(Plot plot, Table table, float value);

}
