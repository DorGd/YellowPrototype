using System;
using UnityEngine;

public interface IInteractable
{
    public Action[] CalcInteractions();
    
    GameObject gameObject { get; }
}
