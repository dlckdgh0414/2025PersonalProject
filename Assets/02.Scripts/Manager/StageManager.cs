using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageSettingSO stageSetting;
    private int ClaerHouse;

    private void Awake()
    {
        ClaerHouse = stageSetting.MaxDeliveryHouse;
    }
}
