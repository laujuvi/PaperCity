using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public class GuiltyRoom : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GuiltyRoomManager guiltyRoomManager;

    private void Start()
    {
        guiltyRoomManager = FindObjectOfType<GuiltyRoomManager>();
    }
    public void Interact()
    {
        if (GameManager.Instance.currentEvidence >= /*guiltyRoomManager.minClue*/GameManager.Instance.uIManager.GetMinClue())
        {
            //guiltyRoomManager.GuiltyRoomUIPanel.SetActive(true);
            GameManager.Instance.uIManager.SetGuiltyRoomVisibility(true);
            //GameManager.Instance.ShowCursor();
            CursorManager.ShowCursor();
            GameManager.Instance.uIManager.PauseGame();
            //guiltyRoomManager.PauseGame();
        }
        else 
        {
            GameManager.Instance.uIManager.GuiltyRoomSendMessage("Detective", Color.white, "Necesito más pistas...", Emotions.None);
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
