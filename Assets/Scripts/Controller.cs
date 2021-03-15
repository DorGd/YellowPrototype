using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAI))]
public class Controller : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Camera _cam;
    private PlayerAI _ai;
    public LayerMask movementMask;
    
    
    [Header("Interactable variables")]
    public LayerMask interactableMas; 
    public float viewRadius = 7f;
    private Interactable _interact; 
    [HideInInspector]
    public List<Transform> visibleInteractable = new List<Transform>();
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _ai = GetComponent<PlayerAI>();
        _cam = Camera.main;
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
        Gizmos.DrawWireSphere(transform.position, viewRadius);
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
            if (Physics.Raycast(ray, out hit, 100f, movementMask))
            {
                // Move player to hit point
                _ai.MoveToPoint(hit.point);

            }
        }

        else if (interactInput)
        {
            Vector2 mousePos = _playerControls.Player.MosuePosition.ReadValue<Vector2>();
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                if (interactable != null)
                {
                    HandleInteraction(interactable, hit.collider);
                }
            }
        }
    }

    
    void HandleInteraction(Interactable interactable, Collider hitCollider)
    {
        // check if the object is close enough to access
        Collider[] interactablesInViewRadius =
            Physics.OverlapSphere(transform.position, viewRadius, interactableMas);

        for (int i = 0; i < interactablesInViewRadius.Length; i++)
        {
            if (hitCollider == interactablesInViewRadius[i])
            {
                // TODO need to add option for multiple interaction options
                interactable.Interact();
            }
        }
    }
}
