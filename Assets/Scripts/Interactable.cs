using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    Chest,
    Helmet,
    MasterKey, 
    HelmetPlace, 
    Wrench, 
    SecurityBadge, 
    Mineral, 
    Vent
}
[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected ItemType CurrItemType;
    protected Outline _outline;

    public void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    
    /**
     * calculate the amount of possible interactions that are possible with the item. 
     */
    public abstract Action[] CalcInteractions();

    /**
     * Get the type of the pbject.
     */
    public ItemType GetType()
    {
        return CurrItemType; 
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Enable();
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        Disable();
    }
    public void Enable()
    {
        _outline.enabled = true;    
    }
    
    public void Disable()
    {
        _outline.enabled = false;    
    }

}
