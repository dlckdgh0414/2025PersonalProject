using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextList", menuName = "SO/Text/TextList")]
public class TextList : ScriptableObject
{
    public List<PicoTextSO> textList;
}
