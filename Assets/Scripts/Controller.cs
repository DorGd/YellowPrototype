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


    private void Awake()
    {
        _playerControls = new PlayerControls();
        _ai = GetComponent<PlayerAI>();
        _cam = Camera.main;
        _leftMouseMask = LayerMask.GetMask("Ground", "Obstruction");
        _rightMouseMask = LayerMask.GetMask("Interactable");
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
                // TODO Need to add distance check

                IInteractable item = hit.transform.gameObject.GetComponent<IInteractable>();
                
               if (item != null)
                {
                    Action[] events = item.CalcInteractions();

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
                        }
                        else
                        {
                            btn.gameObject.SetActive(false);
                        }
                    }
                    
                    rightClickCanvas.transform.position = hit.point;
                    rightClickCanvas.enabled = true;
                }
            }
        }
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
