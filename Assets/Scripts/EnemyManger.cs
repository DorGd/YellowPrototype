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
        //_paradigms = GetComponent<InitRoutine>().Init();
        _field = GetComponent<FieldOfView>();
        _ai = GetComponent<PlayerAI>();
        GameManager.Instance.Clock.TickEvent += UpdateParadigm;

        // Find the current paradigm
        int time = GameManager.Instance.Clock.GetHour();
        for (int i = 0; i < _paradigms.Length; i++)
        {
            if (_paradigms[i].startTime <= time && time <= _paradigms[i].endTime)
            {
                curr = i;
                break;
            }
        }
        //_paradigms[curr].action.Act(this);
        //GetComponent<Patrol>().ChangeRoute(_paradigms[curr].patrolPath);
        //GetComponent<Patrol>().StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (_field.inField(GameObject.FindGameObjectWithTag("Player")))
        {
            //checkRegulation();
        }
    }

    void checkRegulation()
    {
        GameObject[] inventory = GameManager.Instance.Inventory.inventoryItems;
        foreach (HeldItemsRegulation reg in _paradigms[curr].regulations)
        {
            if (reg.isValid(inventory))
            {
                Debug.Log(reg.GetSeverity());
                break;
            }
        }
    }

    void UpdateParadigm()
    {
        int time = GameManager.Instance.Clock.GetHour();
        if (_paradigms[curr].startTime == time)
        {
            _paradigms[curr].action.Act(this);
            curr = (curr + 1) % _paradigms.Length;
        }
    }


}
