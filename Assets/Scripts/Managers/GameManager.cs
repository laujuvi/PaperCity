using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* MANAGERS */
    [Header("MANAGERS")]
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private UIManager uIManager;

    /* PLAYER */
    [Header("PLAYER")]
    public PlayerInteract _playerInteract;
    public PlayerController _playerController;

    /* EVIDENCE */
    [Header("EVIDENCE")]
    [SerializeField] private GameObject[] evidenceArray;

    /* EVIDENCE MONITORING */
    [Header("EVIDENCE MONITORING")]
    [SerializeField] private int minEvidence = 1;
    [SerializeField] private string intermediateMessage;
    [SerializeField] private int maxEvidence = 2;
    [SerializeField] private string definitiveMessage;
    public int currentEvidence = 0;
    public int totalEvidence = 0;

    /* UI */
    [Header("UI")]
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject lenIcon;

    /* NPC */
    [Header("NPC")]
    [SerializeField] private GameObject guiltyNPC;
    public List<GameObject> npcInteracted = new List<GameObject>();

    /* NPC INFO */
    private bool isNPCTalking = false;
    private string lastNPCName;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        win.SetActive(false);
        lose.SetActive(false);

        uIManager.UpdateTotalEvidence(dialogManager.GetTotalEvidence());
        HideCursor();

        dialogManager.SetMinEvidenceForPhase2(minEvidence);
        dialogManager.SetMinEvidenceForPhaseFinal(maxEvidence);
        dialogManager.SetEvidenceArray(evidenceArray);

        maxEvidence = evidenceArray.Length;
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
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisablePlayerController()
    {
        _playerController.enabled = false;
    }
    public void EnablePlayerController()
    {
        _playerController.enabled = true;
    }
    public void DisablePlayerInputs()
    {
        StartCoroutine(DisablePlayerInputsCoroutine());
    }

    private IEnumerator DisablePlayerInputsCoroutine()
    {
        DisablePlayerController();

        yield return new WaitUntil(() => boxMessageManager.IsDisplayingMessage());

        while (boxMessageManager.IsDisplayingMessage())
        {
            yield return null;
        }

        EnablePlayerController();
    }

    public void CheckGuiltyNPC()
    {
        if (guiltyNPC.name == lastNPCName)
        {
            Debug.Log("WIN");
            lenIcon.SetActive(false);
            win.SetActive(true);
            return;
        }
        else
        {
            Debug.Log("LOSE");
            lenIcon.SetActive(false);
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
            boxMessageManager.SendMessage("Detective", Color.white, definitiveMessage, Emotions.None);
            return;
        }

        if (currentEvidence >= minEvidence)
        {
            boxMessageManager.SendMessage("Detective", Color.white, intermediateMessage, Emotions.None);
            return;
        }
    }
    public void SetNPCName(string NPCName)
    {
        lastNPCName = NPCName;
        isNPCTalking = true;
    }

}
