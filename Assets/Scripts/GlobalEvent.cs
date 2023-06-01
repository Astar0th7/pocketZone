using UnityEngine;
using UnityEngine.Events;

public class GlobalEvent : MonoBehaviour
{
    public static UnityEvent<float> OnHealthChengeEvent = new UnityEvent<float>();

    public static void HealthChenge(float hpNormalize)
    {
        OnHealthChengeEvent.Invoke(hpNormalize);
    }
}
