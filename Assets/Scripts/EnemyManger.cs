using UnityEngine;

public class EnemyManger : MonoBehaviour
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

    void Update()
    {
        if (_field.inField(GameObject.FindGameObjectWithTag("Player")))
        {
            foreach (var reg in _paradigms[curr].regulations)
            {
                if(!reg.CheckRegulation()) reg.sanction.Apply();
            }
        }
    }

    void UpdateParadigm()
    {
        int time = GameManager.Instance.Clock.GetHour();
        if (_paradigms[(curr + 1) % _paradigms.Length].startTime == time)
        {
            curr = (curr + 1) % _paradigms.Length;
            // Stop patroling Action if activated
            _ai.Patroling = false;                                          
            // Takes paradigm new path if not null
            if (_paradigms[curr].patrolPath != null)
            {
                _ai.WayPoints = _paradigms[curr].patrolPath.Points;
            }

            _paradigms[curr].action.Act(this);

        }
    }


}
