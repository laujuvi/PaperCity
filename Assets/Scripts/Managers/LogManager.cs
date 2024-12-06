using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    [Header("Log UI")]
    [SerializeField] TextMeshProUGUI logText;

    [Header("Log Object")]
    //[SerializeField] GameObject logObject;
    public bool isLogOpen = false;

    private List<MessageData> messageLog = new List<MessageData>();
    [SerializeField] UIManager uiManager;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Update()
    {
        OpenLogView();
    }
    public void AddMessage(MessageData message)
    {
        messageLog.Add(message);
        DisplayMessagesInUI();
    }
    public void DisplayMessagesInUI()
    {
        logText.text = "";

        // Recorremos los mensajes en orden inverso
        for (int i = messageLog.Count - 1; i >= 0; i--)
        {
            var message = messageLog[i];

            string colorHex = ColorUtility.ToHtmlStringRGB(message.Color);

            logText.text += $"<color=#{colorHex}>{message.Name}</color>: {message.Message}\n";
        }
    }
    public void ClearLog()
    {
        messageLog.Clear();
    }
    private void OpenLogView()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            isLogOpen = !isLogOpen;

            if (isLogOpen)
            {
                CursorManager.ShowCursor();
                GameManager.Instance.DisablePlayerController();
                uiManager.SetLogObjectVisibility(true);
            }
            if (!isLogOpen)
            {
                CursorManager.HideCursor();
                GameManager.Instance.EnablePlayerController();
                uiManager.SetLogObjectVisibility(false);
            }
        }
    }
}