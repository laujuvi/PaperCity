using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private TextMeshProUGUI interactText;

    private void Update()
    {
        if(_playerInteract.GetInteractableObject() != null && !GameManager.Instance.uIManager.IsDisplayingMessage()/*!boxMessageManager.IsDisplayingMessage()*/ )
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
        GameManager.Instance.uIManager.SetContainerGameobjectVisibility(true);
        interactText.text = interactable.GetInteractText();
    }

    private void Hide()
    {
        GameManager.Instance.uIManager.SetContainerGameobjectVisibility(false);
    }
}