using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* MANAGERS */
    [Header("MANAGERS")]
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] public UIManager uIManager;
    [SerializeField] private DataCollector dataCollector;

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
    [SerializeField] public int maxEvidence = 2;
    [SerializeField] private string definitiveMessage;
    public int currentEvidence = 0;
    public int totalEvidence = 0;

    /* UI */
    //[Header("UI")]
    //[SerializeField] private ScoreScreenManager scoreScreen;

    /* NPC */
    [Header("NPC")]
    [SerializeField] private GameObject guiltyNPC;
    [SerializeField] public int totalNPCs; // Este entero se usa para validar cuantos npcs hay en la escena.
    public List<GameObject> npcInteracted = new List<GameObject>();

    /* NPC INFO */
    private string lastNPCName;

    /* PUERTAS */
    [Header("EXIT DOORS")]
    [SerializeField] private GameObject leftExitDoor;
    [SerializeField] private GameObject rightExitDoor;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource musicSc;

    public bool isNoteBookPickedUp = true;
    private bool isGuiltyCheck = false;
    private bool playerHasAcused = false;
    public bool isDisplayingMessage = false;

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
        uIManager.SetWinScreenVisibility(false);
        uIManager.SetLoseScreenVisibility(false);

        if (uIManager != null && dialogManager != null)
        {
            uIManager.UpdateTotalEvidence(dialogManager.GetTotalEvidence());

            dialogManager.SetMinEvidenceForPhase2(minEvidence);
            dialogManager.SetMinEvidenceForPhaseFinal(maxEvidence);
            dialogManager.SetEvidenceArray(evidenceArray);
            maxEvidence = evidenceArray.Length;
        }
        CursorManager.HideCursor();
    }

    private void Update()
    {
        if (isGuiltyCheck) {
            if (!isDisplayingMessage) {
                CheckGuiltyNPC();
                isGuiltyCheck = false;
            }
        }

        isDisplayingMessage = boxMessageManager.IsDisplayingMessage();
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

        uIManager.SetLenIconVisibility(false);

        CursorManager.ShowCursor();

        playerHasAcused = true;

        if (guiltyNPC.name == lastNPCName)
        {
            Debug.Log("WIN");
            uIManager.SetWinScreenVisibility(true);
            audioManager.PlaySoundFX(AudioManager.instance.victorySound, transform, 1f);
            StartCoroutine(GoToScoreScreen());
        }
        else
        {
            Debug.Log("LOSE");
            uIManager.SetLoseScreenVisibility(true);
            audioManager.PlaySoundFX(AudioManager.instance.defeatSound, transform, 1f);
        }
        musicSc.Stop();
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

        uIManager.SetWinScreenVisibility(false);
        uIManager.SetLoseScreenVisibility(false);

        uIManager.SetPreScoreScreenVisibility(true);

        yield return new WaitForSeconds(2f);

        uIManager.SetPreScoreScreenVisibility(false);

        //Hago que el collector junte la data seteada
        dataCollector.SetAllData();

        // Le paso esa data al score
        uIManager.SetScoreValues(dataCollector.GetAllData());
        //scoreScreen.SetScoreValues(dataCollector.GetAllData());

        // Llamo al score para que renderice la data que le pase
        uIManager.UpdateScorePanel();
        //scoreScreen.UpdateScorePanel();

        //scoreScreen.gameObject.SetActive(true);
        uIManager.SetScoreScreenVisibility(true);
    }

}
