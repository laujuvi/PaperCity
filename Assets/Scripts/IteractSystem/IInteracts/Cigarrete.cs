using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigarrete : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject postProcess;
    [SerializeField] private bool isOff;


    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        print("hola");
        postProcess.gameObject.SetActive(false);
        isOff = true;

        if (isOff)
        {
            postProcess.SetActive(true);
            isOff = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
