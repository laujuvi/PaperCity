using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public class GuiltyRoom : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GuiltyRoomManager guiltyRoomManager;
    [SerializeField] UIManager uiManager;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        guiltyRoomManager = FindObjectOfType<GuiltyRoomManager>();
    }
    public void Interact()
    {
        if (GameManager.Instance.currentEvidence >= /*guiltyRoomManager.minClue*/uiManager.GetMinClue())
        {
            //guiltyRoomManager.GuiltyRoomUIPanel.SetActive(true);
            uiManager.SetGuiltyRoomVisibility(true);
            //GameManager.Instance.ShowCursor();
            CursorManager.ShowCursor();
            uiManager.PauseGame();
            //guiltyRoomManager.PauseGame();
        }
        else 
        {
            uiManager.GuiltyRoomSendMessage("Detective", Color.white, "Necesito más pistas...", Emotions.None);
            //guiltyRoomManager.boxMessageManager.SendMessage("Detective", Color.white, "Necesito más pistas...", Emotions.None);
        }
    }
    public string GetInteractText()
    {
        return interactText;
    }
    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }
}
