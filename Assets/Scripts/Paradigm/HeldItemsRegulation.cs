using UnityEngine;

public class HeldItemsRegulation : RegulationSO
{
    // TODO change to GameObject to Collectable
    [SerializeField]
    private GameObject[] _requiredEquipment;
    [SerializeField]
    private GameObject[] _forbiddenEquipment;

    public override bool CheckRegulation()
    {
        foreach (GameObject eq in GameManager.Instance.Inventory.inventoryItems)
        {
            foreach (GameObject feq in _forbiddenEquipment) {
                if (feq.Equals(eq))
                {
                    return false;
                }
            }
        }

        bool reqFlag;
        foreach (GameObject req in _requiredEquipment)
        {
            reqFlag = false;
            foreach (GameObject eq in _requiredEquipment)
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
