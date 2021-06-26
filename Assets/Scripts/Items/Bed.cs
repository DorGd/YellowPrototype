using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    public override Action[] CalcInteractions()
    {
        return new Action[] { Sleep };
    }

    // Start is called before the first frame update
    void Sleep()
    {
        
    }
}
