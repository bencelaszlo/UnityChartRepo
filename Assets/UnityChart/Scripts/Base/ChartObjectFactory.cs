// system
using System;
// unity
using UnityEngine;

namespace UnityChart
{
namespace Base
{

public class ChartObjectFactory : MonoBehaviour
{
    // Chart Prefabs
    [Header("Chart Prefabs")]
    public GameObject ChartHolderPrefab;
    public GameObject ChartLinePrefab;

    private GameObject InstantiateObject(GameObject prefab, Transform parent, String name)
    {
        GameObject instance = Instantiate(prefab, parent);
        instance.name = name;
        return instance;
    }

    // Instantiate Chart Holder
    public GameObject InstantiateChartHolder(Transform parent, String name)
    {
        return InstantiateObject(ChartHolderPrefab, parent, name);
    }

    // Instantiate Chart Line
    public GameObject InstantiateChartLine(Transform parent, String name)
    {
        return InstantiateObject(ChartLinePrefab, parent, name);
    }
}

}
}
