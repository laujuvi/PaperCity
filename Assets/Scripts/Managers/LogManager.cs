using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogManager : MonoBehaviour
{

    [Header("Log UI")]
    [SerializeField] TextMeshProUGUI logText;

    [Header("Log Object")]
    [SerializeField] GameObject logObject;
    public bool isLogOpen = false;

    private List<MessageData> messageLog = new List<MessageData>();

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
                //GameManager.Instance.ShowCursor();
                CursorManager.ShowCursor();
                GameManager.Instance.DisablePlayerController();
                logObject.SetActive(true);
            }

            if (!isLogOpen)
            {
                //GameManager.Instance.HideCursor();
                CursorManager.HideCursor();
                GameManager.Instance.EnablePlayerController();
                logObject.SetActive(false);
            }

        }

    }
}
