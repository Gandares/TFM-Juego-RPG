using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Scriptable Objects/InGameEvent")]
public class InGameEventSO : ScriptableObject
{
    [Header("Event Checker")]
    public bool EventDone;
}