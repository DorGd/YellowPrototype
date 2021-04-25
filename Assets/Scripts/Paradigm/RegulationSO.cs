using UnityEngine;

public abstract class RegulationSO : ScriptableObject
{
    [SerializeField]
    private SanctionSO _sanction;
    public abstract bool CheckRegulation();
}
