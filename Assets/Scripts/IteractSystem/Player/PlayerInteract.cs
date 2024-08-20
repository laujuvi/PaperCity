using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //[SerializeField] private float _InteractRange = 0.1f;
    [SerializeField] private GameObject _RaycastPoint;
    [SerializeField] private float _RaycastDistance;
    [SerializeField] private float _RaycastDistance_2;
    [SerializeField] private LayerMask interactableLayerMask_1;
    [SerializeField] private LayerMask interactableLayerMask_2;
    [SerializeField] private BoxMessageManager _boxMessageManager;
    private PlayerController _playerController;
    private DialogManager _dialogManager;

    private int _currentLayerMask;

    private void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (_playerController.isOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IInteractable interactable = GetInteractableObject();
                if (_boxMessageManager.IsDisplayingMessage()) _boxMessageManager.CheckSkipDialog();
                
                if (interactable != null)
                {
                    if (!_boxMessageManager.IsDisplayingMessage()) interactable.Interact();
                }

            }
        }
       
        Debug.DrawRay(_RaycastPoint.transform.position, _RaycastPoint.transform.forward * _RaycastDistance, Color.red);
        Debug.DrawRay(_RaycastPoint.transform.position, _RaycastPoint.transform.forward * _RaycastDistance_2, Color.blue);
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> InteractableList = new List<IInteractable>();

        Ray ray = new Ray(_RaycastPoint.transform.position, _RaycastPoint.transform.forward);
        Ray ray_2 = new Ray(_RaycastPoint.transform.position, _RaycastPoint.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, _RaycastDistance, interactableLayerMask_1);
        RaycastHit[] hits_2 = Physics.RaycastAll(ray_2, _RaycastDistance_2, interactableLayerMask_2);

        IInteractable InteractableObject = null;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
               
                InteractableObject = interactable;
                //return interactable;
                _currentLayerMask = hit.collider.gameObject.layer;

                
            }
        }
        foreach (RaycastHit hit_2 in hits_2)
        {
            if (hit_2.collider.TryGetComponent(out IInteractable interactable1))
            {
                InteractableObject = interactable1;
                _currentLayerMask = hit_2.collider.gameObject.layer;

            }
        }

        return InteractableObject;
        //return null;
    }
}
