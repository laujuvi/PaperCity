using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private string pickableText;
    [SerializeField] private bool isFakeEvidence;
    [SerializeField] private string description;

    private BoxMessageManager boxMessageManager;
    private DialogManager dialogManager;
    private GameManager gameManager;
    private ListManager _listManager;

    public void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();
        dialogManager = FindObjectOfType<DialogManager>();
        gameManager = FindObjectOfType<GameManager>();
        _listManager = FindObjectOfType<ListManager>();
    }
    public void Interact()
    {
        if (gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            gameManager.DisablePlayerInputs();
            dialogManager.DisplayDialog(gameObject.name);
            Debug.Log("INTERACTUASTE");
        } else if (gameObject.layer == LayerMask.NameToLayer("Pickeable"))
        {
            boxMessageManager.SendMessage("", Color.white, pickableText, Emotions.None);
            if (!isFakeEvidence) { 
            dialogManager.SetEvidenceStatus(gameObject.name, true);
            _listManager.AddText(/*gameObject.name +*/ $"({description})");
            gameManager.CheckCurrentEvidence();
                print("pick");
            gameObject.SetActive(false);
            }
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

    public bool GetisFakeEvidence()
    {
        return isFakeEvidence;
    }

}
