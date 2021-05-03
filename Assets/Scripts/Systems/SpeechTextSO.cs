using UnityEngine;

[CreateAssetMenu(fileName="New Speech", menuName="Speech Text")]
public class SpeechTextSO : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] sentences;
    
}
