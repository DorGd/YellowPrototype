using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Sanctions/Confiscate")]
public class ConfiscateSanction : SanctionSO
{
    [SerializeField]
    private ItemType[] _forbiddenEquipment;

    [SerializeField]
    private SpeechTextSO text;
    public override void Apply(EnemyManager enemy)
    {
        if (text != null)
            GameManager.Instance.SpeechManager.StartSpeech(enemy.transform.position, text);
        foreach (ItemType feq in _forbiddenEquipment)
        {
            if (GameManager.Instance.inventory.IsInInventory(feq))
            {
                Interactable item = GameManager.Instance.inventory.GetItem(feq);
                AudioManager.Instance.PlayOneShot(AudioManager.SFX_failedInteraction, 0.5f);
                GameManager.Instance.inventory.DeleteItem(feq);
                item.ResetPos();
            }
        }
    }
}
