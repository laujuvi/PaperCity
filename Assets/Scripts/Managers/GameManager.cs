using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* MANAGERS */
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private UIManager uIManager;
    private NPCInteractable nPCInteractable;

    /* WAIT */
    public PlayerInteract _playerInteract;
    public PlayerController _playerController;

    /* EVIDENCE MONITORING */
    [SerializeField] private int minEvidence = 1;
    [SerializeField] private int maxEvidence = 2;
    [SerializeField] private GameObject guiltyNPC;
    [SerializeField] private string intermediateMessage;
    [SerializeField] private string definitiveMessage;
    public int currentEvidence = 0;
    public int totalEvidence = 0;

    /* NPC IMFO */
    private bool isNPCTalking = false;
    private string lastNPCName;

    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;

    private void Start()
    {
        win.SetActive(false);
        lose.SetActive(false);
        if (_playerInteract != null)
        {
            _playerInteract.OnInteract += DisablePlayerInputs;
        }
        else
        {
            Debug.LogWarning("PlayerInteract component not found in the scene.");
        }

        uIManager.UpdateTotalEvidence(dialogManager.GetTotalEvidence());
    }

    private void Update()
    {
        if (isNPCTalking)
        {
            if (currentEvidence < minEvidence)
            {
                isNPCTalking = false;
                return;
            }

            if (!boxMessageManager.IsDisplayingMessage())
            {
                CheckGuiltyNPC();
                isNPCTalking = false;
            }
        }
    }

    private void DisablePlayerInputs()
    {
        StartCoroutine(DisablePlayerInputsCoroutine());
    }

    private IEnumerator DisablePlayerInputsCoroutine()
    {
        _playerInteract.enabled = false;
        _playerController.enabled = false;

        yield return new WaitUntil(() => boxMessageManager.IsDisplayingMessage());

        while (boxMessageManager.IsDisplayingMessage())
        {
            yield return null;
        }

        _playerInteract.enabled = true;
        _playerController.enabled = true;
    }

    public void CheckGuiltyNPC()
    {
        if (guiltyNPC.name == lastNPCName)
        {
            Debug.Log("WIN");
            win.SetActive(true);
            return;
        }
        else
        {
            Debug.Log("LOSE");
            lose.SetActive(true);
            return;
        }
    }

    public void CheckCurrentEvidence()
    {
        currentEvidence++;
        uIManager.UpdateCurrentEvidence(currentEvidence);
        if (currentEvidence >= maxEvidence)
        {
            boxMessageManager.SendMessage("", Color.white, definitiveMessage, Emotions.None);
            return;
        }

        if (currentEvidence >= minEvidence)
        {
            boxMessageManager.SendMessage("", Color.white, intermediateMessage, Emotions.None);
            return;
        }
    }

    public IEnumerator DisableNPCInteractable()
    {
        _playerInteract.enabled = false;

        yield return new WaitUntil(() => boxMessageManager.IsDisplayingMessage());

        while (boxMessageManager.IsDisplayingMessage())
        {
            yield return null;
        }

        _playerInteract.enabled = true;

    }

    public void DisableNPCInteractableCorrutine(NPCInteractable interactable)
    {
        StartCoroutine(DisableNPCInteractable());

    }

    public void SetNPCName(string NPCName)
    {
        lastNPCName = NPCName;
        isNPCTalking = true;
    }
}
