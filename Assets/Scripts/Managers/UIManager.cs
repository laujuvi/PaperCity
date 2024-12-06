using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Clues UI")]
    [SerializeField] TextMeshProUGUI cluesText;
    private string defaultCluesText = "Clues found:";
    private int currentEvidence = 0;
    private int totalEvidence = 0;

    [Header("Gameobjects UI")]
    [SerializeField] GameObject lenIcon;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject options;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject GuiltyRoomUIPanel;
    [SerializeField] GameObject logObject;
    [SerializeField] GameObject _containerGameObject;
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    [SerializeField] GameObject preScoreScreen;

    [Header("References UI")]
    [SerializeField] SceneLoadManager sceneLoadManager;
    [SerializeField] GuiltyRoomManager guiltyRoomManager;
    public void UpdateCurrentEvidence(int currentInt)
    {
        currentEvidence = currentInt;
        cluesText.text = $"{defaultCluesText} {currentEvidence}";
    }
    public void UpdateTotalEvidence(int totalInt)
    {
        totalEvidence = totalInt;
        cluesText.text = cluesText.text = $"{defaultCluesText} {currentEvidence}";
    }
    public void SetMenuUIVisibility(bool isVisible)
    {
        if (menuUI != null)
        {
            menuUI.SetActive(isVisible);
        }
    }
    public void SetOptionsVisibility(bool isVisible)
    {
        if (options != null)
        {
            options.SetActive(isVisible);
        }
    }
    public void SetControlsVisibility(bool isVisible)
    {
        if (controls != null)
        {
            controls.SetActive(isVisible);
        }
    }
    public void SetLenIconVisibility(bool isVisible)
    {
        if(lenIcon != null)
        {
            lenIcon.SetActive(isVisible);
        }
    }
    public void SetGuiltyRoomVisibility(bool isVisible)
    {
        if (GuiltyRoomUIPanel != null)
        {
            GuiltyRoomUIPanel.SetActive(isVisible);
        }
    }
    public void SetLogObjectVisibility(bool isVisible)
    {
        if (logObject != null)
        {
            logObject.SetActive(isVisible);
        }
    }
    public void SetWinScreenVisibility(bool isVisible)
    {
        if (win != null)
        {
            win.SetActive(isVisible);
        }
    }
    public void SetLoseScreenVisibility(bool isVisible)
    {
        if (lose != null)
        {
            lose.SetActive(isVisible);
        }
    }
    public void SetContainerGameobjectVisibility(bool isVisible)
    {
        if (_containerGameObject != null)
        {
            _containerGameObject.SetActive(isVisible);
        }
    }
    public void SetPreScoreScreenVisibility(bool isVisible)
    {
        if (preScoreScreen != null)
        {
            preScoreScreen.SetActive(isVisible);
        }
    }
    public int GetMinClue()
    {
        return guiltyRoomManager.minClue;
    }
    public void PauseGame()
    {
        guiltyRoomManager.PauseGame();
    }
    public void GuiltyRoomSendMessage(string name, Color color, string message, Enum none)
    {
        guiltyRoomManager.boxMessageManager.SendMessage(name, color, message, (Emotions)none);
    }
    public void LoadNextScene()
    {
        sceneLoadManager.LoadNextScene();
    }
}