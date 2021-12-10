using UnityEngine;
using UnityEngine.UI;

public class CircleSlider_Amp : CircleSliderBase {

    public override void UpdatePlot(Plot plot, Table table, float value) {


            float amplitude = table.GetAmplitude();
            float amplitude_new = Mathf.Round(value);
            //table.PlotGraphFromInterval(plot, xMin, xMax);
            if (amplitude_new != amplitude) {
            table.ChangeAmplitude((int)(System.Math.Round(amplitude_new,0)));
            table.RequestData(plot);
            }

        GameManager.manager.TrackSliderValue("amp", "" + value);
    }

    
		
}
