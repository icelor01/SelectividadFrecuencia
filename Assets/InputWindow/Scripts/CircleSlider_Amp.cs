// Code extracted and modified from Unity UI Knob Control https://youtu.be/yRKJdUAWn5A
using UnityEngine;
using UnityEngine.UI;

public class CircleSlider_Amp : MonoBehaviour {

    [SerializeField] Transform handle;
    [SerializeField] Image fill;
    [SerializeField] Text valText;
    Vector3 mousePos;
    [SerializeField] public GameObject plotComponent;

        public void onHandleDrag () {
        mousePos = Input.mousePosition;
        Vector2 dir = mousePos - handle.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;
        if (angle<=225 || angle >= 315) {
            Quaternion r = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
            handle.rotation = r;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount =0.75f - (angle / 360f);
            valText.text = Mathf.Round((fill.fillAmount * 100)/(20 *0.75f)).ToString();
           
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            int amplitude = table.GetAmplitude();
            int amplitude_new = (int) Mathf.Round((fill.fillAmount * 100) / (20 * 0.75f));
            //table.PlotGraphFromInterval(plot, xMin, xMax);
            if (amplitude_new != amplitude) {
            table.ChangeAmplitude(amplitude_new);
            table.RequestData(plot);
            }
        }
		
	}
}
