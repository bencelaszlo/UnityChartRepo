// system
using System.Collections.Generic;
// unity
using UnityEngine;
// local
using UnityChart.Base;

namespace UnityChart
{
namespace Extensions
{

public class MarkerInteractableChart : InteractableChartBase
{
    public List<Color> MarkerColorList = new List<Color>();
    public List<int> MarkerIndexList = new List<int>();

    private List<GameObject> markerPointList = new List<GameObject>();

    private Transform linesContainer;

    private void Initialize()
    {
        // Get chart line object
        GameObject[] chartLines = GameObject.FindGameObjectsWithTag("ChartLine");
        int numberOfChartLines = chartLines.Length;
        linesContainer = chartLines[0].transform.parent;

        // Get object factory
        ExtensionsObjectFactory factory = GameObject.FindObjectOfType<ExtensionsObjectFactory>();

        // Clean marker point list
        markerPointList.Clear();

        // Instantiate marker points
        for (int i = 0; i < MarkerIndexList.Count; ++i)
        {
            // Get marker color
            Color color = Color.white; // default white
            if (i < MarkerColorList.Count)
            {
                color = MarkerColorList[i];
            }

            // Instantiate marker with default position
            GameObject marker = null;
            if (numberOfChartLines == 1)
            {
                marker = factory.InstantiateInteractiveChartPointMarker(linesContainer, "PointMarker", new Vector3(0.0f, 0.0f, - 4.0f), color);
            }
            else
            {
                marker = factory.InstantiateInteractiveChartLineMarker(linesContainer, "LineMarker", new Vector3(0.0f, 0.0f, -4.0f));
            }
            markerPointList.Add(marker);
        }
    }

    // Override chart update interaction
    protected override void OnUpdateInteraction()
    {
        if (!linesContainer)
        {
            Initialize();
        }

        for (int i = 0; i < markerPointList.Count; ++i)
        {
            // Update marker properties
            markerPointList[i].GetComponent<MarkerBase>().UpdateMarker(MarkerIndexList[i]);
        }
    }

    public void ModifyMarkerPointIndex(params int[] indices)
    {
        if (indices.Length != MarkerIndexList.Count)
        {
            return;
        }

        for (int i = 0; i < indices.Length; ++i)
        {
            MarkerIndexList[i] = indices[i];
        }

        OnUpdateInteraction();
    }
}

}
}
