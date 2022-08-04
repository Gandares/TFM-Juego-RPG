using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

public class EventChecker : MonoBehaviour
{
    public InGameEventSO[] events;
    public UnityEvent EventImpact;
    private int total;

    void Start()
    {
        if (events.Length > 0)
        {
            total = events.Length;
            int contador = 0;
            for (int i=0; i<events.Length; i++)
            {
                if (events[i].EventDone == true)
                {
                    contador++;
                }
            }

            if(contador == total)
            {
                if (EventImpact != null)
                    eventImpact();
            }
        }
    }

    public void eventImpact()
    {
        EventImpact.Invoke();
    }
}
