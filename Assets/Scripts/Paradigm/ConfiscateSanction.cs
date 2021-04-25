using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Sanctions/Confiscate")]

public class ConfiscateSanction : SanctionSO
{
    // TODO Change to collectable or items enum type
    [SerializeField]
    private GameObject[] _forbiddenEquipment;
    public override void Apply()
    {
        for (int i = 0; i < GameManager.Instance.inventory.Count; ++i)
        {
            // TODO change to collectable
            GameObject eq = GameManager.Instance.inventory.inventoryItems[i];
            foreach (GameObject feq in _forbiddenEquipment)
            {
                if (feq.name.Equals(eq.name))
                {
                    GameManager.Instance.inventory.RemoveItem(eq);
                }
            }
        }
    }
}
