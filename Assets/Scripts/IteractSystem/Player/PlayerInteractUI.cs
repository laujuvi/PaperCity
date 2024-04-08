using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject _containerGameObject;
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private TextMeshProUGUI interactText;

    private BoxMessageManager boxMessageManager;

    public void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();

        if (boxMessageManager == null)
        {
            Debug.LogError("No se encontró un BoxMessageManager en la escena.");
            return;
        }
    }

    private void Update()
    {
        if(_playerInteract.GetInteractableObject() != null && !boxMessageManager.IsDisplayingMessage() )
        {
            Show(_playerInteract.GetInteractableObject());
        } 
        else
        {
            Hide();
        }
    }

    private void Show(IInteractable interactable)
    {
        _containerGameObject.SetActive(true);
        interactText.text = interactable.GetInteractText();
    }

    private void Hide()
    {
        _containerGameObject.SetActive(false);
    }

}
