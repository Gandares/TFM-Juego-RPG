using UnityEngine;
using ScriptableObjectArchitecture;

public class SaveInGameEvent : MonoBehaviour
{
    [Header("Configuration")]
    public InGameEventSO InGameEvent;

    public void InGameEventCompleted()
    {
        InGameEvent.EventDone = true;
    }

    public void InGameEventNotCompleted()
    {
        InGameEvent.EventDone = false;
    }
}
