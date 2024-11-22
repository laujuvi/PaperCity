using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialInteract2 : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private string ObjectName;

    public event Action OnInteractableActivated;

    public GameObject notebookIcon;

    public string GetInteractText()
    {
        return interactText;
        notebookIcon.SetActive(true);
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        gameObject.SetActive(false);

        OnInteractableActivated?.Invoke();
    }
}
