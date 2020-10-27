using UnityEngine;
using UnityEngine.UI;

public class CircleSlider_Span : CircleSliderBase {

    public override void UpdatePlot (Plot plot, Table table, float value) {
        GameManager.instance.TrackSliderValue("span", ""+value);

        table.ChangeSpan(value);
        table.RequestData(plot);
    }
}