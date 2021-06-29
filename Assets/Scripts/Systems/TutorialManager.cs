using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private readonly string[] intro = {
        "Welcome to DAY ONE at Cosmos X",
        "Left mouse click -> Walk\nRight mouse click -> Interact\nSpace -> Highlight Interactable Items",
        "Got it?"
    };
    
    private readonly string[] inventory = {
        "Company items are valuable!",
        "That's why CosmosX has equipped\n you with state-of-the-art inventory.",
        "Which can be found on the left bottom corner."
    };
    
    private readonly string[] handItem = {
        "Some items are carried by hand.\nYou can view your hand item at the top left corner.",
        "You can only carry one item in your hands.",
        "To drop it, simply Right Click anywhere and choose \"Drop\"",
        "Be careful! Guards can see what item you're holding!"
    };
    
    private readonly string[] exchange = {
        "Some items have their own inventory",
        "Hide an item from your inventory or from your hand by clicking on it.",
        "Bring an item back into your inventory by clicking on it"
    };
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.numDay > 1)
        {
            Destroy(this);
        }
        else
        {
            GameManager.Instance.inventory.onFirstTimeUse += IntroduceInventory;
            GameManager.Instance.inventory.onFirstTimeHandItemUse += IntroduceHandObject;
            GameManager.Instance.inventory.GetInventoryUI().onFirstExchange += IntroduceExchange;
            StartCoroutine(IntroduceControls());
        }
    }
    
    private IEnumerator IntroduceControls()
    {
        yield return new WaitForSeconds(7f);
        GameManager.Instance.SpeechManager.StartSpeech(GameManager.Instance.PlayerTransform.position, intro, true);
    }
    
    private void IntroduceInventory()
    {
        GameManager.Instance.inventory.onFirstTimeUse -= IntroduceInventory;
        GameManager.Instance.SpeechManager.StartSpeech(GameManager.Instance.PlayerTransform.position, inventory, true);

    }
    
    private void IntroduceHandObject()
    {
        GameManager.Instance.inventory.onFirstTimeHandItemUse -= IntroduceHandObject;
        GameManager.Instance.SpeechManager.StartSpeech(GameManager.Instance.PlayerTransform.position, handItem, true);
    }
    
    private void IntroduceExchange()
    {
        GameManager.Instance.inventory.GetInventoryUI().onFirstExchange -= IntroduceExchange;
        GameManager.Instance.SpeechManager.StartSpeech(GameManager.Instance.PlayerTransform.position, exchange, true);
    }
}
