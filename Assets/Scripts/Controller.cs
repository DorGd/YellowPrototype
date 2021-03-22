using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


[RequireComponent(typeof(PlayerAI))]
public class Controller : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Camera _cam;
    private PlayerAI _ai;
    private LayerMask _leftMouseMask;
    private LayerMask _rightMouseMask;
    private Button[] _buttons;

    public Canvas rightClickCanvas;
    public GameObject securityBadge;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _ai = GetComponent<PlayerAI>();
        _cam = Camera.main;
        _leftMouseMask = LayerMask.GetMask("Ground", "Obstruction");
        _rightMouseMask = LayerMask.GetMask("Interactable", "Obstruction", "Ground");
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


        if (goInput)
        {
            Vector2 mousePos = _playerControls.Player.MosuePosition.ReadValue<Vector2>();
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, 100f, _leftMouseMask) && !IsMouseOverUI())
            {
                rightClickCanvas.enabled = false;
                switch (LayerMask.LayerToName(hit.transform.gameObject.layer))
                {
                    case "Obstruction":
                        return;
                    case "Ground":
                        // Move player to hit point
                        _ai.MoveToPoint(hit.point);
                        break;
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
                switch (LayerMask.LayerToName(hit.transform.gameObject.layer))
                {
                    case "Obstruction":
                        return;
                    case "Ground":
                        Button btn1 = _buttons[0];
                        Button btn2 = _buttons[1];
                        btn1.onClick.RemoveAllListeners();
                        btn2.onClick.RemoveAllListeners();
                        Text txt1 = btn1.GetComponentInChildren<Text>();
                        Text txt2 = btn2.GetComponentInChildren<Text>();
                        if (GameManager.Instance.Inventory.GetHandItem() != null)
                        {
                            txt1.text = "Drop";
                            btn1.onClick.AddListener(delegate { DropHandItem(); });
                            btn1.onClick.AddListener(delegate { DisableRightClickCanvas(); });
                        }
                        else
                        {
                            txt1.text = "";
                        }
                        if (GameManager.Instance.Inventory.IsInInventory(securityBadge, false))
                        {
                            txt2.text = "Drop Badge";
                            btn2.onClick.AddListener(delegate { DropBadge(); });
                            btn2.onClick.AddListener(delegate { DisableRightClickCanvas(); });
                        }
                        else
                        {
                            txt2.text = "";
                        }
                        rightClickCanvas.transform.position = hit.point;
                        rightClickCanvas.enabled = true;
                        return;
                }
                // TODO Need to add distance check

                IInteractable item = hit.transform.gameObject.GetComponent<IInteractable>();
                
               if (item != null)
                {
                    Action[] events = item.CalcInteractions();
                    
                    // empty the buttons
                    Button btn1 = _buttons[0];
                    Button btn2 = _buttons[1];
                    btn1.onClick.RemoveAllListeners();
                    btn2.onClick.RemoveAllListeners();
                    Text txt1 = btn1.GetComponentInChildren<Text>();
                    Text txt2 = btn2.GetComponentInChildren<Text>();
                    txt1.text = "";
                    txt2.text = "";

                    if (events.Length >= 1) // first method
                    {
                        txt1.text = events[0].Method.Name;
                        btn1.onClick.RemoveAllListeners();
                        btn1.onClick.AddListener(delegate { events[0](); });
                        btn1.onClick.AddListener(delegate { DisableRightClickCanvas(); });
                    }
                    if (events.Length == 2) // second method
                    {
                        txt2.text = events[1].Method.Name;
                        btn2.onClick.RemoveAllListeners();
                        btn2.onClick.AddListener(delegate { events[1](); });
                        btn2.onClick.AddListener(delegate { DisableRightClickCanvas(); });
                    }

                    rightClickCanvas.transform.position = hit.point;
                    rightClickCanvas.enabled = true;
                }
            }
        }
    }

    private void DropBadge()
    {
        GameManager.Instance.Inventory.RemoveItem(securityBadge);
        securityBadge.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up;
        securityBadge.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.Find("Badge").gameObject.SetActive(false);
    }

    private void DropHandItem()
    {
        GameManager.Instance.Inventory.AddItem(null, true);
    }

    private void DisableRightClickCanvas()
    {
        rightClickCanvas.enabled = false;
    }

    private void foo(int i)
    {
        Debug.Log("plzzz" + i);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();    
    }
}
