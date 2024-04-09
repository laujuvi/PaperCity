using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PruebaInteractuable_2 : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
