using UnityEngine;

[CreateAssetMenu(fileName = "PicoTextSO", menuName = "SO/Text/PicoText")]
public class PicoTextSO : ScriptableObject
{
   [TextArea]
   public string[] picotexts;
}
