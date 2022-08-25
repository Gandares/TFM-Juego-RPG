using UnityEngine;
using ScriptableObjectArchitecture;

public class SceneLoader : MonoBehaviour
{
    [Header("Configuration")]
    public EntranceSceneToLoadSO EntranceSceneToLoad;
    public bool loadingScreen;

    [Header("Player Path")]
    public PlayerPathSO playerPath;

    [Header("Broadcasting events")]
    public LoadSceneRequestGameEvent loadSceneEvent;

    public void LoadScene()
    {
        if (this.EntranceSceneToLoad.levelEntrance != null && this.playerPath != null)
            this.playerPath.levelEntrance = this.EntranceSceneToLoad.levelEntrance;

        var request = new LoadSceneRequest(
            scene: this.EntranceSceneToLoad.Scene,
            loadingScreen: this.loadingScreen
        );

        this.loadSceneEvent.Raise(request);
    }
}
