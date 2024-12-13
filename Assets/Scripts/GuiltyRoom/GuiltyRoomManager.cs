using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class GuiltyRoomManager : MonoBehaviour
{
    /* Guilty Room Sttings */
    [Header("GuiltyRoom Settings\n")]
    [SerializeField] private List<NPCInteractable> npcs = new List<NPCInteractable>();
    [SerializeField] private List<Transform> tpPoints = new List<Transform>();
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject playerController;
    [SerializeField] private CharacterController characterController;
    [SerializeField] public BoxMessageManager boxMessageManager;
    [SerializeField] public int minClue;

    [Header("GuiltyRoom UI Settings\n")]

    [SerializeField] public GameObject GuiltyRoomUIPanel;
    private bool isPaused;
    public GameObject lenIcon;

    private float guiltyRoomStartTime;
    private float totalTimeInGuiltyRoom;

    string crouch = "crouch";
    string lookUp = "lookUp";
    void Start()
    {
        boxMessageManager = FindAnyObjectByType<BoxMessageManager>();
        minClue = GameManager.Instance.GetMinEvidence();
    }
    public void GoToGuiltyRoomYes()
    {
        StartGuiltyRoomTimer();

        GameManager.Instance.SentActionUsedEvents(crouch, GameManager.Instance.crouch);
        GameManager.Instance.SentActionUsedEvents(lookUp, GameManager.Instance.lookUp);

        if (playerController != null)
        {
            characterController.enabled = false;
            playerController.gameObject.transform.position = playerTransform.position;
            characterController.enabled = true;
            GameManager.Instance.isPlayerInGuiltyRoom = true;
            GameManager.Instance.enableDisableOutlineDoors(false);
        }

        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].transform.position = tpPoints[i].transform.position;
            npcs[i].changeGuiltyText();
        }
        GuiltyRoomUIPanel.gameObject.SetActive(false);
        GameManager.Instance.HideCursor();
        ResumeGame();
    }

    public void GoToGuiltyRoomNo()
    {
        GuiltyRoomUIPanel.gameObject.SetActive(false);
        GameManager.Instance.HideCursor();
        ResumeGame();
    }

    public void ResumeGame()
    {
        lenIcon.SetActive(true);
        GameManager.Instance.EnablePlayerController();
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        lenIcon.SetActive(false);
        GameManager.Instance.DisablePlayerController();
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void StartGuiltyRoomTimer()
    {
        guiltyRoomStartTime = Time.time;
    }
    public void StopGuiltyRoomTimer()
    {
        totalTimeInGuiltyRoom = Time.time - guiltyRoomStartTime;
        float timeSpent = GetGuiltyRoomTime();
        GameManager.Instance.SentGuiltyRoomTimeEvents(timeSpent);
    }
    public float GetGuiltyRoomTime()
    {
        return totalTimeInGuiltyRoom;
    }
}
