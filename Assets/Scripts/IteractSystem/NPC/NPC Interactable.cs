using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    public JamesMoriartyKeyClue jamesMoriartyKeyClue;
    public int countDialog;
    [SerializeField] private string interactText;
    [SerializeField] private string pickablePJText = "";
    [SerializeField] private string pickableText;
    [SerializeField] private bool isFakeEvidence;
    [SerializeField] private string description;

    private BoxMessageManager boxMessageManager;
    private DialogManager dialogManager;
    
    [Header("ListManager")]
    private ListManager _listManager;
    [SerializeField] private string clueName;

    [Header("Audio Source")]
    [SerializeField] private AudioManager audioManager;

    public void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();
        dialogManager = FindObjectOfType<DialogManager>();
        _listManager = FindObjectOfType<ListManager>();
        //clueName = gameObject.name;
    }
    public void Interact()
    {
        if (gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            if (!GameManager.Instance.isPlayerInGuiltyRoom && GameManager.Instance.currentEvidence >= GameManager.Instance.minEvidence) {
                GameManager.Instance.isNPCTalking = true;
                return;
            } 
            GameManager.Instance.DisablePlayerInputs();
            dialogManager.DisplayDialog(gameObject.name);
            if (gameObject.name == "James Moriarty")
            {
                countDialog++;
                jamesMoriartyKeyClue.CheckCountDialog(countDialog);
            }
            Debug.Log("INTERACTUASTE");
            if (!GameManager.Instance.npcInteracted.Contains(gameObject))
            {
                GameManager.Instance.npcInteracted.Add(gameObject);
            }
        }
        //} else if (gameObject.layer == LayerMask.NameToLayer("Pickeable"))
        //{
        //    boxMessageManager.SendMessage(pickablePJText, Color.white, pickableText, Emotions.None);
        //    if (!isFakeEvidence)
        //    { 
        //        dialogManager.SetEvidenceStatus(gameObject.name, true);
        //        _listManager.AddText(clueName + $"({description})");
        //        gameManager.CheckCurrentEvidence();
        //        audioManager.PlaySFX(audioManager.clueFound);
        //        print("pick");
        //        //gameObject.SetActive(false);
        //        Destroy(gameObject);
        //    }
        //}
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
