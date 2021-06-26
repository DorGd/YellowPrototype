using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    public ItemType _type;
    public Sprite[] sprites;

    public ItemType GETItemType()
    {
        return _type;
    }

    public Sprite GetSprite(int spriteIndex)
    {
        return sprites[spriteIndex];
    }
}
