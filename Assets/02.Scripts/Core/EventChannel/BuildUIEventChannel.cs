using UnityEngine;

public class BuildUIEventChannel :MonoBehaviour
{
    public static BuildObjectUI BuildObjectUI = new BuildObjectUI();
}

public class BuildObjectUI : GameEvent
{
    public int buildCost;
    public GameObject buildObject;

    public BuildObjectUI Initializer(GameObject buildObject,int buildCost)
    {
        this.buildObject = buildObject;
        this.buildCost = buildCost;
        return this;
    }
}