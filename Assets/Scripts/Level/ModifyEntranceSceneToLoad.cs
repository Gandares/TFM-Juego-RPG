using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyEntranceSceneToLoad : MonoBehaviour
{
    public SceneSO Scene;
    public LevelEntranceSO LevelEntrance;
    public EntranceSceneToLoadSO EntranceSceneToLoad;

    public void Modify()
    {
        EntranceSceneToLoad.Scene = this.Scene;
        EntranceSceneToLoad.levelEntrance = this.LevelEntrance;
    }
}
