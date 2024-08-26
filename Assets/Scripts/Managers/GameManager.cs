using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* MANAGERS */
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private UIManager uIManager;

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

    /* INITIAL DIALOGS */
    [SerializeField] string[] lines;


    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;

    [SerializeField] private GameObject lenIcon;

    public List<GameObject> npcInteracted = new List<GameObject>();

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

        StartDialogue();

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
    public void DisablePlayerInputs()
    {
        StartCoroutine(DisablePlayerInputsCoroutine());
    }

    private IEnumerator DisablePlayerInputsCoroutine()
    {
        //_playerInteract.enabled = false;
        _playerController.enabled = false;

        yield return new WaitUntil(() => boxMessageManager.IsDisplayingMessage());

        while (boxMessageManager.IsDisplayingMessage())
        {
            yield return null;
        }

        //_playerInteract.enabled = true;
        _playerController.enabled = true;
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
            boxMessageManager.SendMessage("", Color.white, definitiveMessage, Emotions.None);
            return;
        }

        if (currentEvidence >= minEvidence)
        {
            boxMessageManager.SendMessage("", Color.white, intermediateMessage, Emotions.None);
            return;
        }
    }
    public void SetNPCName(string NPCName)
    {
        lastNPCName = NPCName;
        isNPCTalking = true;
    }

    void StartDialogue()
    {
        foreach (string line in lines)
        {
            boxMessageManager.SendMessage("", Color.white, line, Emotions.None);
        }
    }

}
