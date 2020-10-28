using UnityEngine;
using UnityEngine.UI;

public class CircleSlider_Freq : CircleSliderBase {

      public override void  UpdatePlot (Plot plot, Table table, float value)    {


        float fc = table.Getfc();
        // fc inicial=0 El dial fc suma el valor indicado de frecuencia

        float fc_new = value;
      
        if ((int)fc_new != (int)fc)
        {
            table.Changefc(fc_new);
            table.RequestData(plot);
        }

        GameManager.manager.TrackSliderValue("freq", "" + value);
    }
}