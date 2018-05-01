// system
using System;
using System.Collections.Generic;
using System.Linq;
// unity
using UnityEngine;
// local
using UnityChart.Base;

namespace UnityChart
{
namespace Extensions
{

public class LineMarker : MarkerBase
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void UpdateMarker(int index)
    {
        base.UpdateMarker(index);

        // Get first chart line component
        ChartLine chartLine = FindObjectOfType<ChartLine>();

        // Get first line renderer component
        LineRenderer lineRenderer = chartLine.GetComponent<LineRenderer>();

        // Get marker position
        Vector3 markerPosition = lineRenderer.GetPosition(index);

        // Set marker object position
        this.gameObject.transform.localPosition = new Vector3(
            markerPosition.x, 0, this.gameObject.transform.localPosition.z);

        // Get max and min values for all the chart lines and the given index
        List<float> yValueSet = new List<float>();
        foreach (ChartLine cl in FindObjectsOfType<ChartLine>())
        {
            yValueSet.Add(cl.Y[index]);
        }
        String minText = yValueSet.Min().ToString("0.00#");
        String maxText = yValueSet.Max().ToString("0.00#");

        // Set text label
        this.gameObject.transform.GetComponentInChildren<TextMesh>().text = "Min: " + minText + " Max: " + maxText;
    }
}

}
}
