using System.Collections;
using System.Collections.Generic;
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
    private bool isPaused;

    void Start()
    {
        boxMessageManager = FindAnyObjectByType<BoxMessageManager>();
        minClue = GameManager.Instance.GetMinEvidence();
    }
    public void GoToGuiltyRoomYes()
    {
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
        //GuiltyRoomUIPanel.gameObject.SetActive(false);
        GameManager.Instance.uIManager.SetGuiltyRoomVisibility(false);
        CursorManager.HideCursor();
        ResumeGame();
    }

    public void GoToGuiltyRoomNo()
    {
        //GuiltyRoomUIPanel.gameObject.SetActive(false);
        GameManager.Instance.uIManager.SetGuiltyRoomVisibility(false);
        CursorManager.HideCursor();
        ResumeGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.uIManager.SetLenIconVisibility(true);
        GameManager.Instance.EnablePlayerController();
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        GameManager.Instance.uIManager.SetLenIconVisibility(false);
        GameManager.Instance.DisablePlayerController();
        Time.timeScale = 0f;
        isPaused = true;
    }
}