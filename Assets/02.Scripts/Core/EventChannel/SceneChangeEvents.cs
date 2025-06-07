using UnityEngine;

public static class SceneChangeEvents
{
    public static SceneChnages SceneChnages = new SceneChnages();
}

public class SceneChnages : GameEvent
{
    public int LoadSceneCount;

    public SceneChnages Initializer(int loadSceneCount)
    {
        LoadSceneCount = loadSceneCount;
        return this;
    }
}
