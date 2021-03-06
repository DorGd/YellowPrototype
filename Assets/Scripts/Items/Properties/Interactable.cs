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

    protected int spriteIndex = 0;
    private Vector3 initialPos;
    private bool mouseOver = false;

    public bool MouseOver { get { return mouseOver; } }

    protected virtual void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.enabled = false;
        initialPos = transform.position;
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
        mouseOver = true;
        _outline.enabled = true;    
    }
    
    public void Disable()
    {
        mouseOver = false;
        if (!GameManager.Instance.IsHighlightInteractables)
            _outline.enabled = false;
    }

    public Sprite GetSprite()
    {
        return item.GetSprite(spriteIndex);
    }

    public void ResetPos()
    {
        transform.position = initialPos;
        gameObject.SetActive(true);
    }

}
