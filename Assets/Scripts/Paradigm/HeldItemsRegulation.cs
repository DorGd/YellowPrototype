using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Regulations/Held Items")]
public class HeldItemsRegulation : RegulationSO
{
    // TODO change to GameObject to Collectable
    [SerializeField]
    private Interactable[] _requiredEquipment;
    [SerializeField]
    private Interactable[] _forbiddenEquipment;

    public override bool CheckRegulation()
    {
        if (GameManager.Instance.inventory.IsEmpty())
        {
            if (_requiredEquipment.Length == 0)
            {
                return true;
            }
            return false;
        }

        for (int i = 0; i < GameManager.Instance.inventory.Count; ++i)
        {
            // TODO change to collectable
            Interactable eq = GameManager.Instance.inventory.inventoryItems[i];
            foreach (Interactable feq in _forbiddenEquipment)
            {
                // TODO change to collectable type
                if (feq.name.Equals(eq.name))
                {
                    return false;
                }
            }
        }

        bool reqFlag;
        foreach (Interactable req in _requiredEquipment)
        {
            reqFlag = false;
            foreach (Interactable eq in _requiredEquipment)
            {
                if (req.Equals(eq))
                {
                    reqFlag = true;
                    break;
                }
            }
            if (!reqFlag)
            {
                return false;
            }
        }
        return true;
    }
}
