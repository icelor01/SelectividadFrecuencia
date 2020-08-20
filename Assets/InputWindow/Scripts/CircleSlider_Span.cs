// Code extracted and modified from Unity UI Knob Control https://youtu.be/yRKJdUAWn5A
using UnityEngine;
using UnityEngine.UI;

public class CircleSlider_Span : MonoBehaviour {

    [SerializeField] Transform handle;
    [SerializeField] Image fill;
    [SerializeField] Text valText;
    Vector3 mousePos;
    [SerializeField] public GameObject plotComponent;
    int xmin_initial;
    int xmax_initial;
    int inc_last=0;

    private void Awake()
    {
      
        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
        xmin_initial = table.Getxmin();
        xmax_initial= table.Getxmax();
    }

      public void onHandleDrag()
    {
        mousePos = Input.mousePosition;
        Vector2 dir = mousePos - handle.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;
        if (angle <= 225 || angle >= 315)
        {
            Quaternion r = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
            handle.rotation = r;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount = 0.75f - (angle / 360f);
            
            valText.text = Mathf.Round((fill.fillAmount * 100) / (0.75f)).ToString();

            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            int xmin = table.Getxmin();
            int xmax = table.Getxmax();
            int inc= (int)Mathf.Round((fill.fillAmount * 100) / (0.75f));
            int xmin_new = xmin + inc;
            int xmax_new = xmax - inc;
            //table.PlotGraphFromInterval(plot, xMin, xMax);
            
           
            if (xmin_new < xmax_new)  { 
            Debug.Log("Este es el incremento: " + inc);
            //Compresión
                if ((inc > inc_last)) {
                table.ChangeSpan(xmin + inc, xmax - inc);
                table.RequestData(plot);
                    inc_last = inc;
                }
           //Expansión
           else{
                table.ChangeSpan(xmin - inc, xmax + inc);
                table.RequestData(plot);
                    inc_last = -inc;
                }

            
        }

        }
        
    }
}
