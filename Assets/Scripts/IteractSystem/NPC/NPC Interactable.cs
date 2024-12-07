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
    private GuiltyRoomManager guiltyRoomManager;
    
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
    }
    public void Interact()
    {
        if (gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            // Antes de avanzar con el dialogo se fija si no deberia mostrarse el mensaje de ir a la sala de interrogatorio
            if (!GameManager.Instance.isPlayerInGuiltyRoom && GameManager.Instance.currentEvidence >= GameManager.Instance.minEvidence) {
                guiltyRoomManager.StopGuiltyRoomTimer();
                GameManager.Instance.CheckPlayerSituation();
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
    }
    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void changeGuiltyText()
    {
        interactText = interactText.Replace("Hablar con ", "Acusar a ");
    }

}
