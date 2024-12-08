using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Clues UI")]
    [SerializeField] TextMeshProUGUI cluesText;
    private string defaultCluesText = "Pistas halladas:";
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
    [SerializeField] GameObject bgDialog;

    [Header("References UI")]
    [SerializeField] SceneLoadManager sceneLoadManager;
    [SerializeField] GuiltyRoomManager guiltyRoomManager;
    [SerializeField] BoxMessageManager boxMessageManager;
    [SerializeField] GameSettings gameSettings;
    [SerializeField] LogManager logManager;
    [SerializeField] ScoreScreenManager scoreScreenManager;
    [SerializeField] NotebookManager notebookManager;

    private void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();
        gameSettings = FindObjectOfType<GameSettings>();

        if (boxMessageManager == null)
        {
            Debug.LogError("No se encontró un BoxMessageManager en la escena.");
            return;
        }
    }
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

    #region GameobjectsUIVisibility
    public void SetMenuUIVisibility(bool isVisible)
    {
        if (menuUI != null)
        {
            menuUI.SetActive(isVisible);
        }
    }

    public bool GetMenuUIVisibility()
    {
        return menuUI.activeSelf;
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
    public void SetBgDialogVisibility(bool isVisible)
    {
        if (bgDialog != null)
        {
            bgDialog.SetActive(isVisible);
        }
    }
    #endregion

    #region GuiltyRoomManager
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
    #endregion

    #region SceneLoadManager
    public void LoadNextScene()
    {
        sceneLoadManager.LoadNextScene();
    }
    #endregion

    #region GameSettings
    public void UpdateSensitivitySlider(Slider mouseSensSlider)
    {
        gameSettings.UpdateSensitivitySlider(mouseSensSlider);
    }
    public void OnDestroyGameSettingsGameobject()
    {
        gameSettings.DestroySelf();
    }
    #endregion

    #region BoxMessageManager
    public bool IsDisplayingMessage()
    {
        return boxMessageManager.IsDisplayingMessage();
    }
    #endregion

    #region ScoreScreenManager
    public void SetScoreValues(DataCollected dataCollected)
    {
        scoreScreenManager.SetScoreValues(dataCollected);
    }
    public void UpdateScorePanel()
    {
        scoreScreenManager.UpdateScorePanel();
    }
    public void SetScoreScreenVisibility(bool isVisible)
    {
        if (scoreScreenManager.gameObject != null)
        {
            scoreScreenManager.gameObject.SetActive(isVisible);
        }
    }
    #endregion

    #region NotebookManager
    public void SetEnableNotebook()
    {
        notebookManager.EnableNotebook();
    }

    public void SetDisabledNotebook()
    {
        notebookManager.DisableNotebook();
    }

    public void SetEnableNotebookIcon()
    {
        notebookManager.EnableNotebookIcon();
    }

    public void SetDisabledNotebookIcon()
    {
        notebookManager.DisableNotebookIcon();
    }

    public bool CheckNotebookStatus()
    {
        return notebookManager.NotebookStatus();
    }
    #endregion
}