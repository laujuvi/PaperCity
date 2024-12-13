using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicClueInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] public string pickablePJText = "Detective";
    [SerializeField] public string pickableText;
    

    private BoxMessageManager boxClueMessageManager;


  
    // Start is called before the first frame update
    void Start()
    {
        boxClueMessageManager = FindObjectOfType<BoxMessageManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Interact()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Pickeable"))
        {         
           GameManager.Instance.fakeClueCount++;
           boxClueMessageManager.SendMessage(pickablePJText, Color.white, pickableText, Emotions.Talking);
        
        }
   
    }
    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }


}
