using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private ParadigmSO[] _paradigms;
    private int curr;
    private FieldOfView _field;
    private PlayerAI _ai;
    public PlayerAI Ai
    {
        get { return _ai; }
    }

    void Start()
    {
        _field = GetComponent<FieldOfView>();
        _ai = GetComponent<PlayerAI>();
        GameManager.Instance.Clock.TickEvent += UpdateParadigm;

        // Find the current paradigm
        int time = GameManager.Instance.Clock.GetHour();
        curr = -1;
        for (int i = 0; i < _paradigms.Length; i++)
        {
            if (_paradigms[i].startTime <= time && time <= _paradigms[i].endTime)
            {
                curr = (i - 1) % _paradigms.Length;
                break;
            }
        }
        UpdateParadigm();
        _field.onEnterField.AddListener(RegulationsValidation);
    }

    void RegulationsValidation()
    {
        foreach (var reg in _paradigms[curr].regulations)
        {
            if(!reg.CheckRegulation()) reg.sanction.Apply();
        }
    }

    public void UpdateParadigm()
    {
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        Debug.Log($"time = {time}, paradigm time = {_paradigms[(curr + 1) % _paradigms.Length].startTime}");
        if (_paradigms[(curr + 1) % _paradigms.Length].startTime == time)
        {
            ActivateNextParadigm();
        }
    }

    public void ActivateNextParadigm()
    {
            float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
            curr = (curr + 1) % _paradigms.Length;
            ParadigmSO nextParadigm = _paradigms[curr];
            if (nextParadigm.startTime <= time && nextParadigm.endTime >= time)
            {    
                // Stop patroling / watch Action if activated
                _ai.Patroling = false;                                          
                // Takes paradigm new path if not null
                if (nextParadigm.patrolPath != null)
                {
                    _ai.WayPoints = nextParadigm.patrolPath.Points;
                }
                Debug.Log(nextParadigm.action.name);
                nextParadigm.action.Act(this);
            }
    }

    public ParadigmSO GetCurrentParadigm()
    {
        return _paradigms[curr];
    }
}
