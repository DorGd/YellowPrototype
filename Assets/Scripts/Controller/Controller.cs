using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Ai))]
public class Controller : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Camera _cam;
    private Ai _ai;
    private LayerMask _leftMouseMask;
    private LayerMask _rightMouseMask;
    private Button[] _buttons;
    private Interactable targetItem = null;
    private Coroutine disableButtonsCoroutine = null;

    public Canvas rightClickCanvas;
    public Animator goToCircleAnimator;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _ai = GetComponent<Ai>();
        _cam = Camera.main;
        _leftMouseMask = LayerMask.GetMask("Ground", "Obstruction");
        _rightMouseMask = LayerMask.GetMask("Ground", "Interactable");
        _buttons = rightClickCanvas.GetComponentsInChildren<Button>(true);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    
    // visualize the radius of interaction
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    
    void Update()
    {
        // Read Left Muse value
        bool goInput = _playerControls.Player.Go.triggered;

        // Read Right Muse value
        bool interactInput = _playerControls.Player.Interact.triggered;

        if (goInput || interactInput)
        {
            disableButtonsCoroutine = StartCoroutine(DisableButtons());
            targetItem = null;
            _ai.StopAgent();
        }
        else if (targetItem != null && (GameManager.Instance.PlayerAI.transform.position - targetItem.transform.position).magnitude <= 2)
        {
            if (disableButtonsCoroutine != null)
                StopCoroutine(disableButtonsCoroutine);
            _ai.StopAgent();
            PresentInteractions(targetItem.transform.position, targetItem);
        }

        if (goInput)
        {
            Vector2 mousePos = _playerControls.Player.MosuePosition.ReadValue<Vector2>();
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, 100f, _leftMouseMask) && !IsMouseOverUI())
            {
                GameManager.Instance.inventory.GetInventoryUI().StopExchange();
                rightClickCanvas.enabled = false;
                switch (LayerMask.LayerToName(hit.transform.gameObject.layer))
                {
                    case "Obstruction":
                        return;
                    case "Ground":
                        // Move player to hit point
                        goToCircleAnimator.SetTrigger("CircleTrigger");
                        goToCircleAnimator.gameObject.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                        _ai.MoveToPoint(hit.point);
                        return;
                }

            }
        }

        else if (interactInput)
        {
            Vector2 mousePos = _playerControls.Player.MosuePosition.ReadValue<Vector2>();
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, 100f, _rightMouseMask))
            {
                GameManager.Instance.inventory.GetInventoryUI().StopExchange();
                Interactable item = hit.transform.gameObject.GetComponent<Interactable>();

                switch (LayerMask.LayerToName(hit.transform.gameObject.layer))
                {
                    case "Interactable":
                        if (item != null)
                        {
                            if ((GameManager.Instance.PlayerAI.transform.position - item.transform.position).magnitude > 2)
                            {
                                // Move player to object
                                goToCircleAnimator.SetTrigger("CircleTrigger");
                                goToCircleAnimator.gameObject.transform.position = item.transform.position + new Vector3(0, 0.1f, 0);
                                _ai.MoveToPoint(item.transform.position);
                                targetItem = item;
                                return;
                            }
                            if (disableButtonsCoroutine != null)
                                StopCoroutine(disableButtonsCoroutine);
                            PresentInteractions(hit.point, item);
                        }
                        return;
                    case "Ground":
                        // Show Drop Interaction if player carries a hand item
                        if (disableButtonsCoroutine != null)
                            StopCoroutine(disableButtonsCoroutine);
                        PresentInteractions(hit.point, null);
                        return;
                }

                
            }
        }
    }

    private void PresentInteractions(Vector3 pos, Interactable item)
    {
        Action[] events;
        if (item == null)
        {
            if (GameManager.Instance.inventory.GetHandItem() == null)
                return;
            events = new Action[] { Drop };
        }
        else
        {
            events = item.CalcInteractions();
        }

        for (int i = 0; i < _buttons.Length; i++)
        {
            Button btn = _buttons[i];
            if (i < events.Length)
            {
                btn.gameObject.SetActive(true);
                btn.onClick.RemoveAllListeners();
                Text txt = btn.GetComponentInChildren<Text>();

                int j = i;
                txt.text = events[j].Method.Name;
                btn.onClick.AddListener(delegate { events[j](); });
                btn.onClick.AddListener(delegate { StartCoroutine(DisableButtons()); });
            }
            else
            {
                btn.gameObject.SetActive(false);
            }
        }

        rightClickCanvas.transform.position = pos;
        rightClickCanvas.enabled = true;
    }

    public void Drop()
    {
        Interactable handItem = GameManager.Instance.inventory.GetHandItem();
        GameManager.Instance.inventory.DeleteItem(handItem.GetItemType());
        handItem.transform.position = GameManager.Instance.PlayerTransform.position;
        handItem.gameObject.SetActive(true);
    }

    private IEnumerator DisableButtons()
    {
        yield return new WaitForSeconds(0.2f);
        foreach(Button btn in _buttons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();    
    }

    public void FreezeController()
    {
        _playerControls.Disable();
        rightClickCanvas.enabled = false; 
    }

    public void UnFreezeController()
    {
        _playerControls.Enable();
    }
}
