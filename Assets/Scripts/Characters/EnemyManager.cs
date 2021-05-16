using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private ParadigmSO[] _paradigms;
    private Stack<ParadigmSO> _eventParadigms;
    private ParadigmSO _currParadigm;
    public ParadigmSO CurrentParadigm
    {
        get { return _currParadigm; }
    }
    private bool _isEvent = false;
    private int curr;
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
    }


    public void UpdateParadigm()
    {
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        // check if next paradigm start time has arrived or current paradigm has expired 
        if (_paradigms[(curr + 1) % _paradigms.Length].startTime == time)
        {
            ActivateNextParadigm();
        }
        else if (curr >= 0 && _paradigms[curr].endTime <= time)
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
            int j = (i + curr) % _paradigms.Length; 
            if (_paradigms[j].endTime >= time)
            {
                curr = (j - 1) % _paradigms.Length;
                return;
            }
        }
    }
    public void LoadEventParadigms(ParadigmSO[] paradigms)
    {
        for (int i = paradigms.Length - 1; i >= 0; --i) _eventParadigms.Push(paradigms[i]);
    }

    public void InvokeEventParadigm()
    {
        if (_eventParadigms.Count > 0)
        {
            _isEvent = true;
            StopAction();
            if (curr >= 0) curr--;
            ActivateNextParadigm();
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
                UpdateParadigm();
            }
            else 
            {
                ParadigmSO eventParadigm = _eventParadigms.Pop();
                _currParadigm = eventParadigm;
                eventParadigm.action.Act(this);
            }
            return;
        }

        // Regular routine logic
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        curr = (curr + 1) % _paradigms.Length;
        ParadigmSO nextParadigm = _paradigms[curr];
        _currParadigm = nextParadigm;
        // TODO handle midnight paradigm shift

        // next paradigm time slot contain current time
        if (nextParadigm.startTime <= time && nextParadigm.endTime >= time)
        {    
            // Stop patroling / watch Action if activated
            _ai.Patroling = false;                                          
            // Takes paradigm new path if not null
            if (nextParadigm.patrolPath != null)
            {
                _ai.WayPoints = nextParadigm.patrolPath.Points;
            }
            if (!_isEvent) CurrentCoroutine =  nextParadigm.action.Act(this);
        }
        else
        {
            Debug.Log($"No relevant paradigm for {gameObject.name}, wait until {nextParadigm.startTime} o'clock.");
        }
    }

    public void StopAction()
    {
        if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
        Ai.StopAgent();
    }
    void RegulationsValidation()
    {
        if (curr < 0) return;
        foreach (var reg in _paradigms[curr].regulations)
        {
            if(!reg.CheckRegulation()) reg.sanction.Apply();
        }
    }
}
