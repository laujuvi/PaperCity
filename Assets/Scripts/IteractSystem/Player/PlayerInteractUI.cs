using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if(_playerInteract.GetInteractableObject() != null && !uiManager.IsDisplayingMessage()/*!boxMessageManager.IsDisplayingMessage()*/ )
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
        uiManager.SetContainerGameobjectVisibility(true);
        interactText.text = interactable.GetInteractText();
    }

    private void Hide()
    {
        uiManager.SetContainerGameobjectVisibility(false);
    }
}