using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class BasicClueInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] public string pickablePJText = "Detective";
    [SerializeField] public string pickableText;
    public bool miniClue;
    private BoxMessageManager boxClueMessageManager;

    void Start()
    {
        boxClueMessageManager = FindObjectOfType<BoxMessageManager>();
    }
    public virtual void Interact()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Pickeable"))
        {         
            GameManager.Instance.fakeClueCount++;
            boxClueMessageManager.SendMessage(pickablePJText, Color.white, pickableText, Emotions.Talking);
            if (miniClue)
            {
                GameManager.Instance.interactions++;
            }
            if (GameManager.Instance.currentEvidence == 0 && GameManager.Instance.fakeClueCount == 0)
            {
                GameManager.Instance.isFirstClue = true;
            }
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