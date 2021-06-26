using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Sanctions/Confiscate")]
public class ConfiscateSanction : SanctionSO
{
    [SerializeField]
    private Interactable[] _forbiddenEquipment;
    public override void Apply(EnemyManager enemy)
    {
        for (int i = 0; i < GameManager.Instance.inventory.Count; ++i)
        {
            // TODO change to collectable
            Interactable eq = GameManager.Instance.inventory.InventoryItems[i];
            foreach (Interactable feq in _forbiddenEquipment)
            {
                if (feq.name.Equals(eq.name))
                {
                    ItemType eqType = eq.GetItemType();
                    GameManager.Instance.inventory.DeleteItem(eqType);
                }
            }
        }
    }
}
