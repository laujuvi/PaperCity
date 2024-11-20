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
    public bool isPlayerInGuiltyRoom = false;
    [SerializeField] private string exitHouseMessage;

    /* EVIDENCE */
    [Header("EVIDENCE")]
    [SerializeField] private GameObject[] evidenceArray;

    /* EVIDENCE MONITORING */
    [Header("EVIDENCE MONITORING")]
    [SerializeField] public int minEvidence = 1;
    [SerializeField] private string intermediateMessage;
    [SerializeField] private int maxEvidence = 2;
    [SerializeField] private string definitiveMessage;
    public int currentEvidence = 0;
    public int totalEvidence = 0;

    /* UI */
    [Header("UI")]
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private ScoreScreenManager scoreScreen;
    [SerializeField] private GameObject lenIcon;

    /* NPC */
    [Header("NPC")]
    [SerializeField] private GameObject guiltyNPC;
    public List<GameObject> npcInteracted = new List<GameObject>();

    /* NPC INFO */
    private string lastNPCName;

    /* PUERTAS */
    [Header("EXIT DOORS")]
    [SerializeField] private GameObject leftExitDoor;
    [SerializeField] private GameObject rightExitDoor;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource musicSc;

    private bool isGuiltyCheck = false;
    private bool playerHasAcused = false;

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
        if (isGuiltyCheck) {
            if (!boxMessageManager.IsDisplayingMessage()) {
                CheckGuiltyNPC();
                isGuiltyCheck = false;
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
        //Meto este validador para que no pase nada si el player spamea el clic una vez que le tiro la pantalla final.
        if (playerHasAcused) return;
        Debug.Log("Checking guilty NPC. GuiltyNPC: " + guiltyNPC.name + " - LastNPCName: " + lastNPCName);
        lenIcon.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (guiltyNPC.name == lastNPCName)
        {
            Debug.Log("WIN");
            win.SetActive(true);
            audioManager.PlaySoundFX(AudioManager.instance.victorySound, transform, 1f);
        }
        else
        {
            Debug.Log("LOSE");
            lose.SetActive(true);
            audioManager.PlaySoundFX(AudioManager.instance.defeatSound, transform, 1f);
        }
        musicSc.Stop();

        playerHasAcused = true;
        StartCoroutine(GoToScoreScreen());

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
        Debug.Log("Setting NPC name to: " + NPCName);
        lastNPCName = NPCName;
        CheckPlayerSituation();
    }

    // Chequea el estado del juego para saber si tiene que seguir buscando pistas o ya puede acusar
    public void CheckPlayerSituation()
    {
        // Si todavia no tiene el minimo de pistas no hace nada
        if (currentEvidence < minEvidence)
        {
            return;
        }

        // Si ya tiene el minimo de pistas pero no esta en la sala de acusacion entonces avisa que tiene que salir de la casa
        if (!isPlayerInGuiltyRoom)
        {
            boxMessageManager.SendMessage("Detective", Color.white, exitHouseMessage, Emotions.None);
            enableDisableOutlineDoors(true);
            return;
        }

        isGuiltyCheck = true;
    }

    public void enableDisableOutlineDoors(bool state)
    {
        leftExitDoor.gameObject.GetComponent<Outline>().enabled = state;
        rightExitDoor.gameObject.GetComponent<Outline>().enabled = state;
    }

    public int GetMinEvidence()
    {
        return minEvidence;
    }

    private IEnumerator GoToScoreScreen()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        win.SetActive(false);
        lose.SetActive(false);

        scoreScreen.SetTotalClues(maxEvidence);
        scoreScreen.SetCluesObtained(currentEvidence);
        scoreScreen.UpdateScorePanel();

        scoreScreen.gameObject.SetActive(true);

    }

}
