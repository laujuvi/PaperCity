using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class GameManager : MonoBehaviour   
{
    /* MANAGERS */
    [Header("MANAGERS")]
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] GuiltyRoomManager guiltyRoomManager;

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

    [Header("Analytics")]
    public int gameplayTime = 0; 
    private float DeltaTime = 0;    
    private bool rightSuspect = false;
    public int fakeClueCount = 0;
    public bool isFirstClue = false;
    public int openNoteBook = 0;
    public bool completedGame = false;
    public bool isLeavingGame = false;
    public int interactions = 0;
    public int crouch = 0;
    public int lookUp = 0;

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
        DeltaTime += Time.deltaTime;
        gameplayTime += ((int)DeltaTime);
        if (isGuiltyCheck) {
            if (!boxMessageManager.IsDisplayingMessage()) {
                CheckGuiltyNPC();
                SentEvents(gameplayTime);
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
        Debug.Log("Checking guilty NPC. GuiltyNPC: " + guiltyNPC.name + " - LastNPCName: " + lastNPCName);
        if (guiltyNPC.name == lastNPCName)
        {
            Debug.Log(guiltyRoomManager.guiltyRoomElapsedTime);
            guiltyRoomManager.StopGuiltyRoomTimer();

            Debug.Log("WIN");
            lenIcon.SetActive(false);
            win.SetActive(true);
            SentEndLevelEvents(true, lastNPCName);

            SentAccusationRoomCluesEvents(interactions);
            SentGuiltyRoomTimeEvents(guiltyRoomManager.GetGuiltyRoomTime());

            SentClueCountEvents(currentEvidence);
            SentOpenNoteBookEvents(openNoteBook);
            SentFakeClueEvents(fakeClueCount, isFirstClue);
            audioManager.PlaySoundFX(AudioManager.instance.victorySound, transform, 1f);
            musicSc.Stop();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        else
        {
            Debug.Log(guiltyRoomManager.guiltyRoomElapsedTime);
            guiltyRoomManager.StopGuiltyRoomTimer();

            Debug.Log("LOSE");
            lenIcon.SetActive(false);
            lose.SetActive(true);
            SentEndLevelEvents(false, lastNPCName);

            SentAccusationRoomCluesEvents(interactions);
            SentGuiltyRoomTimeEvents(guiltyRoomManager.GetGuiltyRoomTime());

            SentClueCountEvents(currentEvidence);
            SentOpenNoteBookEvents(openNoteBook);
            SentFakeClueEvents(fakeClueCount, isFirstClue);
            audioManager.PlaySoundFX(AudioManager.instance.defeatSound, transform, 1f);
            musicSc.Stop();
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
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

        // Si ya esta listo para acusar se busca al culpable
        if (!boxMessageManager.IsDisplayingMessage())
        {
            guiltyRoomManager.StopGuiltyRoomTimer();
            SentAccusationRoomCluesEvents(interactions);
            CheckGuiltyNPC();
        }
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

    public void SentEvents(int gameplayTime)
    {
        GameplayTime btnEvt = new GameplayTime
        {
            gameplay_Time = gameplayTime,
        };

        AnalyticsService.Instance.RecordEvent(btnEvt);
    } 
    public void SentFirstClueEvents(int gameplayTime, string clueID)
    {
        FirstClue btnEvt = new FirstClue
        {
            gameplay_Time = gameplayTime,
            clue_ID = clueID
        };

        AnalyticsService.Instance.RecordEvent(btnEvt);
    }
    public void SentFakeClueEvents(int fakeClueCount, bool isFirstClue)
    {
        FakeClueCounter btnEvt = new FakeClueCounter
        {
            fake_Clue_Count = fakeClueCount,
            is_First_Clue = isFirstClue
        };

        AnalyticsService.Instance.RecordEvent(btnEvt);
    }
    public void SentClueCountEvents(int clueCount)
    {
        ClueDiscovery btnEvt = new ClueDiscovery
        {
            clue_Count = clueCount
        };

        AnalyticsService.Instance.RecordEvent(btnEvt);
    }
    
    public void SentEndLevelEvents(bool correctSuspectRate, string suspectID)
    {
        CorrectSuspectRate btnEvt = new CorrectSuspectRate
        {
            correct_Suspect_Rate = correctSuspectRate,

            suspect_ID = suspectID
        };
        completedGame = true;
        AnalyticsService.Instance.RecordEvent(btnEvt);
    }

    public void SentOpenNoteBookEvents(int TimesPlayeropenNotebook)
    {
        Open_Notebook btnEvt = new Open_Notebook
        {
            open_NoteBook = TimesPlayeropenNotebook,
        };

        AnalyticsService.Instance.RecordEvent(btnEvt);
    }

    public void SentEndGamelEvents(int gameplayTime, bool leaveGame)
    {
        Game_Abandoment btnEvt = new Game_Abandoment
        {
            gameplay_Time = gameplayTime,

            Leave_Game = leaveGame
        };

        AnalyticsService.Instance.RecordEvent(btnEvt);
    }
    public void SentGuiltyRoomTimeEvents(float timeSpentInGuiltyRoom)
    {
        AccusationDecisionTime Evt = new AccusationDecisionTime
        {
            decisionTime = timeSpentInGuiltyRoom,
        };
        AnalyticsService.Instance.RecordEvent(Evt);
        AnalyticsService.Instance.Flush();
    }
    public void SentAccusationRoomCluesEvents(int _interactions)
    {
        AccusationRoomClues Evt = new AccusationRoomClues
        {
            accusationRoomClues = _interactions,
        };
        AnalyticsService.Instance.RecordEvent(Evt);
        AnalyticsService.Instance.Flush();
    }
    public void SentActionUsedEvents(string actionName, int actionUsed)
    {
        ActionUsed Evt = new ActionUsed
        {
            actionName = actionName,
            actionUsedTimes = actionUsed
        };
        AnalyticsService.Instance.RecordEvent(Evt);
        AnalyticsService.Instance.Flush();
    }
}
