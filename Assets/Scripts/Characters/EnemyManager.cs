using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private ParadigmSO[] _paradigms;
    private Stack<ParadigmSO> _eventParadigms;
    [SerializeField]
    private ParadigmSO _currParadigm;
    public ParadigmSO CurrentParadigm
    { 
        get { return _currParadigm; }
    }
    private bool _isEvent = false;
    private bool _isRoutinePaused = false;
    private int _curr;
    private FieldOfView _field;
    private Ai _ai;
    public Ai Ai
    {
        get { return _ai; }
    }
    private Coroutine _currentCoroutine;
    public Coroutine CurrentCoroutine
    {
        get { return _currentCoroutine;}
        set { _currentCoroutine = value;}
    }

    void Start()
    {
        _ai = GetComponent<Ai>();
        _eventParadigms = new Stack<ParadigmSO>();
        _field = GetComponent<FieldOfView>();
        _field.onEnterField.AddListener(RegulationsValidation);
        GameManager.Instance.Clock.TickEvent += UpdateParadigm;

        // Find the current paradigm
        int time = GameManager.Instance.Clock.GetHour();
        _curr = -1;
        for (int i = 0; i < _paradigms.Length; i++)
        {
            if (_paradigms[i].startTime <= time && time < _paradigms[i].endTime)
            {
                _curr = (i - 1) % _paradigms.Length;
                break;
            }
        }
        ActivateNextParadigm();
    }

    # region Paradigm Routine Management 

    public void UpdateParadigm()
    {
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        int nextDayFactor = 0;
        if (_currParadigm.startTime > _currParadigm.endTime) nextDayFactor = 24;  // handle situation: [start=22, time=23, end=4] -> [start=22, time=23, end=28]

        // check if next paradigm start time has arrived or current paradigm has expired 
        if (_paradigms[(_curr + 1) % _paradigms.Length].startTime == time)
        {
            ActivateNextParadigm();
        }
        else if (_curr >= 0 && _paradigms[_curr].endTime + nextDayFactor <= time && !_isEvent)
        {
            FindNextParadigm();
            ActivateNextParadigm();
        }
    }

    void FindNextParadigm()
    {
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();

        for (int i = 0; i < _paradigms.Length; i++)
        {
            int j = (i + _curr) % _paradigms.Length; 
            if (_paradigms[j].endTime >= time)
            {
                _curr = (j - 1) % _paradigms.Length;
                return;
            }
        }
    }

    public void ResumeCurrentParadigm()
    {
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        
        // day factor is added to handle day shift cases
        int nextDayFactor = 0;
        if (_currParadigm.startTime > _currParadigm.endTime) nextDayFactor = 24;  // handle situation: [start=22, time=23, end=4] -> [start=22, time=23, end=28]
        if (time < _currParadigm.endTime && nextDayFactor != 0) time += nextDayFactor; // handle situation: [start=22, time=2, end=4] -> [start=22, time=26, end=28]
        
        // current paradigm time slot contain current time
        if (_currParadigm.startTime <= time && _currParadigm.endTime + nextDayFactor >= time)
        {    
            // Stop patroling / watch Action if activated
            _ai.Patroling = false;                                          
            // Takes paradigm new path if not null
            if (_currParadigm.patrolPath != null)
            {
                _ai.WayPoints = _currParadigm.patrolPath.Points;
            }
            if (!_isRoutinePaused) CurrentCoroutine =  _currParadigm.action.Act(this);
        }
        else
        {
            Debug.Log($"No relevant paradigm for {gameObject.name}, wait until {_currParadigm.startTime} o'clock.");
        }
    }
    public void ActivateNextParadigm()
    {   
        // Event based paradigm logic    
        if (_isEvent)
        {
            if (_eventParadigms.Count == 0) 
            {
                _isEvent = false;
                ResumeCurrentParadigm();
            }
            else 
            {
                ParadigmSO eventParadigm = _eventParadigms.Pop();
                _currParadigm = eventParadigm;
                if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = eventParadigm.action.Act(this);
                Debug.Log($"{gameObject.name} invoke paradigm: {_currParadigm.name}.");
            }
            return;
        }

        // Regular routine logic
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        _curr = (_curr + 1) % _paradigms.Length;
        ParadigmSO nextParadigm = _paradigms[_curr];
        _currParadigm = nextParadigm;

        // day factor is added to handle day shift cases
        int nextDayFactor = 0;
        if (nextParadigm.startTime > nextParadigm.endTime) nextDayFactor = 24;  // handle situation: [start=22, time=23, end=4] -> [start=22, time=23, end=28]
        if (time < nextParadigm.endTime && nextDayFactor != 0) time += nextDayFactor; // handle situation: [start=22, time=2, end=4] -> [start=22, time=26, end=28]
        
        // next paradigm time slot contain current time
        if (nextParadigm.startTime <= time && nextParadigm.endTime + nextDayFactor >= time)
        {    
            // Stop patroling / watch Action if activated
            _ai.Patroling = false;                                          
            // Takes paradigm new path if not null
            if (nextParadigm.patrolPath != null)
            {
                _ai.WayPoints = nextParadigm.patrolPath.Points;
            }
            if (!_isRoutinePaused)
            {
                if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
                if (nextParadigm.action == null)  Debug.LogWarning($"No ActionSO provided for {nextParadigm.name}!");
                CurrentCoroutine =  nextParadigm.action.Act(this);
            } 
        }
        else
        {
            Debug.Log($"No relevant paradigm for {gameObject.name}, wait until {nextParadigm.startTime} o'clock.");
        }
    }

    # endregion

    public void LoadEventParadigms(ParadigmSO[] paradigms)
    {
        for (int i = paradigms.Length - 1; i >= 0; --i) _eventParadigms.Push(paradigms[i]);
    }

    public void InvokeEventParadigm()
    {
        if (_eventParadigms.Count > 0)
        {
            _isEvent = true;
            PauseAgentRoutine();
            ActivateNextParadigm();
        }
    }
    public void PauseAgentRoutine()
    {
        if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
        Ai.StopAgent();
        _isRoutinePaused = true;
    }

    public void ResumeAgentRoutine()
    {
        _isRoutinePaused = false;
    }

    void RegulationsValidation()
    {
        if (_curr < 0) return;
        foreach (var reg in _paradigms[_curr].regulations)
        {
            if(!reg.CheckRegulation()) reg.sanction.Apply();
        }
    }
}
