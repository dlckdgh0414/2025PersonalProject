using UnityEngine;

[CreateAssetMenu(fileName = "BuildObject", menuName = "SO/Build/BuildObject")]
public class BuildObjectSO : ScriptableObject
{
    public Sprite BuildIcon;
    public RoadPrefab BuildObject;
    public int BuildCost;
}
