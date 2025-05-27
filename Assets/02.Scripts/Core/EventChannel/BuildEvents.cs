using UnityEngine;

public static class BuildEvents
{
    public static BuildObject BuildObject = new BuildObject();
    public static BuildObjectCheck BuildObjectCheck = new BuildObjectCheck();
}

public class  BuildObject : GameEvent
{
    public int buildCost;
    public bool IsUseCost;
    public BuildObject Initializer(int buildCost,bool isUseCost = false)
    {
        this.buildCost = buildCost;
        this.IsUseCost = isUseCost;
        return this;
    }
}

public class BuildObjectCheck : GameEvent
{
    public bool IsBuild = true;
    public BuildObjectCheck Initializer(bool isBuild)
    {
        this.IsBuild = isBuild;
        return this;
    }
}
