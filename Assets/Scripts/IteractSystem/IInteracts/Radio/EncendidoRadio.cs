using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncendidoRadio : MonoBehaviour, IInteractable  
{
    RadioInteract radio;
    [SerializeField] private string interactText;

    public string GetInteractText()
    {
        return interactText;
    }

    // Start is called before the first frame update
    void Start()
    {
        radio = FindObjectOfType<RadioInteract>();
    }

    public void Interact()
    {
        if (!radio.isRadioON)
        {
            radio.isRadioON = true;
        }
        else
        {
            radio.isRadioON = false;
        }
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }
}
