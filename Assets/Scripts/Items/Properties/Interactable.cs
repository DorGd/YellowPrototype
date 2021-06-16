using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    [SerializeField]
    public bool isHandItem;
    // protected ItemType CurrItemType;
    protected Outline _outline;

    protected virtual void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.enabled = false;
    }
    
    /**
     * calculate the amount of possible interactions that are possible with the item. 
     */
    public abstract Action[] CalcInteractions();

    /**
     * Get the type of the pbject.
     */
    public ItemType GetItemType()
    {
        return item.GETItemType(); 
    }
    
    // handel outline
    
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
