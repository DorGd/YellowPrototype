using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    public ItemType _type;
    public Sprite sprite;

    public ItemType GETItemType()
    {
        return _type;
    }
}
