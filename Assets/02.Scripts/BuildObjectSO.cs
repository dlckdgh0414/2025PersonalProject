using UnityEngine;

[CreateAssetMenu(fileName = "BuildObject", menuName = "SO/Build/BuildObject")]
public class BuildObjectSO : ScriptableObject
{
    public Sprite BuildIcon;
    public GameObject BuildObject;
    public int BuildCost;
}
