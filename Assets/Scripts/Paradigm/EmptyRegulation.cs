using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Regulations/Empty")]
public class EmptyRegulation : RegulationSO
{
    
    [SerializeField] bool isTrue = false;
    public override bool CheckRegulation()
    {
        return isTrue;
    }

}

