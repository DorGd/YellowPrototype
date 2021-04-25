using UnityEngine;

public abstract class RegulationSO : ScriptableObject
{
    public SanctionSO sanction;
    public abstract bool CheckRegulation();
}
