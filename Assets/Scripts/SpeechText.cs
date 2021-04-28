using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName="New Speech", menuName="Speech Text")]
public class SpeechText : ScriptableObject
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
        SpeechText temp = JsonUtility.FromJson<SpeechText>(jsonFile);
        jsonFile = temp.jsonFile;
        sentences = temp.sentences;
    }
}
