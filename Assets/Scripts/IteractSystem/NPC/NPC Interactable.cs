using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string interactText;
    [SerializeField] private string pickableText;

    private BoxMessageManager boxMessageManager;
    private DialogManager dialogManager;

    public void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();
        dialogManager = FindObjectOfType<DialogManager>();

        if (boxMessageManager == null)
        {
            Debug.LogError("No se encontró un BoxMessageManager en la escena.");
            return;
        }
    }


    public void Interact()
    {
        if (gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            dialogManager.DisplayDialog(gameObject.name);
            Debug.Log("INTERACTUASTE");
        } else if (gameObject.layer == LayerMask.NameToLayer("Pickeable"))
        {
            dialogManager.SetEvidenceStatus(gameObject.name, true);
            boxMessageManager.SendMessage("", Color.white, pickableText, Emotions.None);
            gameObject.SetActive(false);
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
