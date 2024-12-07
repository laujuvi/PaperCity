using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicClueInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] public string pickablePJText = "Detective";
    [SerializeField] public string pickableText;
    [SerializeField] string clueName;

    private BoxMessageManager boxClueMessageManager;

    void Start()
    {
        boxClueMessageManager = FindObjectOfType<BoxMessageManager>();

    }
    public virtual void Interact()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Pickeable"))
        {         
            boxClueMessageManager.SendMessage(pickablePJText, Color.white, pickableText, Emotions.Talking);
            
        }
    }
    private void SendClueAnalytics(string clueName)
    {
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