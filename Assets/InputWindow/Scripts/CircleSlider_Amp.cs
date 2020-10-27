using UnityEngine;
using UnityEngine.UI;

public class CircleSlider_Amp : CircleSliderBase {

    public override void UpdatePlot(Plot plot, Table table, float value) {
            GameManager.instance.TrackSliderValue("amp", ""+value);

            int amplitude = table.GetAmplitude();
            int amplitude_new = (int) Mathf.Round(value);
            //table.PlotGraphFromInterval(plot, xMin, xMax);
            if (amplitude_new != amplitude) {
            table.ChangeAmplitude(amplitude_new);
            table.RequestData(plot);
            }
        }
		
}
