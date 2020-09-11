using UnityEngine;
using UnityEngine.UI;

public class CircleSlider_Span : CircleSliderBase {

    public void Awake()  {
        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
        int xmin = table.Getxmin();
        int xmax = table.Getxmax();
        int actual_span = xmax - xmin;

        //Debo modificar la posición de la manilla (handle) para que tome el valor de actual_span

    }

    public override void UpdatePlot (Plot plot, Table table, float value) {

        table.ChangeSpan(value);
        table.RequestData(plot);

    }

}