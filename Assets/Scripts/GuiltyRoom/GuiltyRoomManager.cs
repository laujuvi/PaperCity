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

    [SerializeField] public GameObject GuiltyRoomUIPanel;
    private bool isPaused;
    public GameObject lenIcon;
    // Start is called before the first frame update
    void Start()
    {
        boxMessageManager = FindAnyObjectByType<BoxMessageManager>();
        minClue = GameManager.Instance.GetMinEvidence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToGuiltyRoomYes()
    {
        if (playerController != null)
        {
            characterController.enabled = false;
            playerController.gameObject.transform.position = playerTransform.position;
            characterController.enabled = true;
        }

        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].transform.position = tpPoints[i].transform.position;
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
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        lenIcon.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
