using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _InteractRange = 0.1f;
    [SerializeField] private GameObject _RaycastPoint;
    [SerializeField] private float _RaycastDistance;
    [SerializeField] private LayerMask interactableLayerMask;
    public event Action OnInteract;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                OnInteract?.Invoke();
                interactable.Interact();
            }          
        }
        Debug.DrawRay(_RaycastPoint.transform.position, _RaycastPoint.transform.forward * _RaycastDistance, Color.red);
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> InteractableList = new List<IInteractable>();

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, _InteractRange);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, _InteractRange, interactableLayerMask);


        IInteractable InteractableObject = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                InteractableObject = interactable;
            }
        }
        return InteractableObject;

    }
}
