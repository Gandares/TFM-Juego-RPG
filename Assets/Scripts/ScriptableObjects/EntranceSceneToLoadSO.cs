using UnityEngine;

[CreateAssetMenu(fileName = "EntranceSceneToLoad", menuName = "Scriptable Objects/EntranceSceneToLoad")]
public class EntranceSceneToLoadSO : ScriptableObject
{
    public SceneSO Scene;
    public LevelEntranceSO levelEntrance;
}