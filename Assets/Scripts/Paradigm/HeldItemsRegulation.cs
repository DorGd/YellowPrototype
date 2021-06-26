using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Regulations/Held Items")]
public class HeldItemsRegulation : RegulationSO
{
    // TODO change to GameObject to Collectable
    [SerializeField]
    private ItemType[] _requiredEquipment;
    [SerializeField]
    private ItemType[] _forbiddenEquipment;

    public override bool CheckRegulation()
    {
        foreach (ItemType feq in _forbiddenEquipment)
        {
            if (GameManager.Instance.inventory.IsInInventory(feq))
            {
                return false;
            }
        }

        foreach (ItemType req in _requiredEquipment)
        {
            if (!GameManager.Instance.inventory.IsInInventory(req))
            {
                return false;
            }
        }

        return true;
    }
}
