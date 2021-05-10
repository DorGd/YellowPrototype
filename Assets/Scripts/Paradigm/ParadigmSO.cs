using UnityEngine;

[CreateAssetMenu(fileName = "Paradigm", menuName = "Paradigm/Empty Paradigm", order = 1)]
public class ParadigmSO : ScriptableObject
{
    public ActionSO action;
    public int startTime;
    public int endTime;
    public Path patrolPath;
    public SpeechTextSO text;
    public Vector3 goToPosition;
    public Vector2 watchSector; // x -> left bound, y -> right bound
    public RegulationSO[] regulations;

    public ParadigmSO(ActionSO _action, int _startTime, int _endTime , Path _patrolPath, SpeechTextSO _text,
                     Vector3 _goToPosition, Vector2 _watchSector, RegulationSO[] _regulations)
    {
        action = _action;
        startTime = _startTime;
        endTime = _endTime;
        patrolPath = _patrolPath;
        text = _text;
        goToPosition = _goToPosition;
        watchSector = _watchSector;
        regulations = _regulations;
    }
}
