using UnityEngine;

[CreateAssetMenu(fileName = "DeilverySO", menuName = "SO/Deilvery/Food")]
public class DeilveryFoodSO : ScriptableObject
{
    public FoodEnum foodType;
    public string foodName;
    public Sprite foodSprite;
}
