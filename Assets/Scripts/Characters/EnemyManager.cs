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
        _field = GetComponent<FieldOfView>();
        _ai = GetComponent<Ai>();
        _eventParadigms = new Stack<ParadigmSO>();
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
        if (curr < 0) return;
        foreach (var reg in _paradigms[curr].regulations)
        {
            if(!reg.CheckRegulation()) reg.sanction.Apply();
        }
    }

    public void UpdateParadigm()
    {
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        if (_paradigms[(curr + 1) % _paradigms.Length].startTime == time)
        {
            ActivateNextParadigm();
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
        if (_isEvent)
        {
            if (_eventParadigms.Count == 0) _isEvent = false;
            else 
            {
                ParadigmSO eventParadigm = _eventParadigms.Pop();
                _currParadigm = eventParadigm;
                eventParadigm.action.Act(this);
                // Tell to RoutineManager that the event is done
                return;
            }
        }

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
        // next paradigm end-time has past -> searching the next relevant paradigm
        else if (nextParadigm.endTime < time)
        {
            // TODO search next relevant paradigm
            Debug.Log("Search next relevant paradigm");
        }
        // else -> go to default paradigm
    }

    public void StopAction()
    {
        if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
        Ai.StopAgent();
    }
}
