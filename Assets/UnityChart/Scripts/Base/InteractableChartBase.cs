// unity
using UnityEngine;
using UnityEngine.Events;

namespace UnityChart
{
namespace Base
{

public class InteractableChartBase : MonoBehaviour
{
    public UnityEvent OnInteractableChartUpdate = new UnityEvent();

    public void UpdateInteraction()
    {
        OnUpdateInteraction();
        OnInteractableChartUpdate.Invoke();
    }

    // Update interaction is called each time the chart is updated
    protected virtual void OnUpdateInteraction()
    {
    }
}

}
}
