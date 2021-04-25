using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Regulations/Held Items")]
public class HeldItemsRegulation : RegulationSO
{
    // TODO change to GameObject to Collectable
    [SerializeField]
    private GameObject[] _requiredEquipment;
    [SerializeField]
    private GameObject[] _forbiddenEquipment;

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
            GameObject eq = GameManager.Instance.inventory.inventoryItems[i];
            foreach (GameObject feq in _forbiddenEquipment)
            {
                // TODO change to collectable type
                if (feq.name.Equals(eq.name))
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
