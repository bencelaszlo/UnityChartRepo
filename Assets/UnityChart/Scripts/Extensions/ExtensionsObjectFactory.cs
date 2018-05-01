// system
using System;
using System.Collections.Generic;
// unity
using UnityEngine;
// local
using UnityChart.Base;

namespace UnityChart
{
namespace Extensions
{

public class ExtensionsObjectFactory : MonoBehaviour
{
    // Chart Extension Prefabs
    [Header("Chart Extension Prefabs")]
    public GameObject InteractiveChartPointMarkerPrefab;
    public GameObject InteractiveChartLineMarkerPrefab;

    private GameObject InstantiateObject(GameObject prefab, Transform parent, String name)
    {
        GameObject instance = Instantiate(prefab, parent);
        instance.name = name;
        return instance;
    }

    // Instantiate Chart Holder
    public GameObject InstantiateInteractiveChartHolder(Transform parent, String name, List<int> markerPointIndices, List<Color> markerPointColors)
    {
        ChartObjectFactory chartFactory = FindObjectOfType<ChartObjectFactory>();
        GameObject chart = chartFactory.InstantiateChartHolder(parent, name);
        MarkerInteractableChart interactiveComponent = chart.AddComponent<MarkerInteractableChart>();
        interactiveComponent.MarkerIndexList = markerPointIndices;
        interactiveComponent.MarkerColorList = markerPointColors;
        return chart;
    }

    // Instantiate Chart Interactive Point Marker
    public GameObject InstantiateInteractiveChartPointMarker(Transform parent, String name, Vector3 position, Color color)
    {
        GameObject chartPoint = InstantiateObject(InteractiveChartPointMarkerPrefab, parent, name);
        chartPoint.transform.localPosition = position;
        chartPoint.transform.GetComponentInChildren<MeshRenderer>().material.color = color;
        return chartPoint;
    }

    // Instantiate Chart Interactive Line Marker
    public GameObject InstantiateInteractiveChartLineMarker(Transform parent, String name, Vector3 position)
    {
        GameObject chartPoint = InstantiateObject(InteractiveChartLineMarkerPrefab, parent, name);
        chartPoint.transform.localPosition = position;
        return chartPoint;
    }
}

}
}
