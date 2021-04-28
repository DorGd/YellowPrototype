using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName="New Speech", menuName="Speech Text")]
public class SpeechTextSO : ScriptableObject
{
    public string jsonFile;

    [TextArea(3, 10)]
    public string[] sentences;

    public void exportJson()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + jsonFile, json);
    }

    public void importJson()
    {
        SpeechTextSO temp = JsonUtility.FromJson<SpeechTextSO>(jsonFile);
        jsonFile = temp.jsonFile;
        sentences = temp.sentences;
    }
}
