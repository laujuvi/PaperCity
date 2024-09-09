using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI logText;

    private List<MessageData> messageLog = new List<MessageData>();

    public void AddMessage(MessageData message)
    {
        messageLog.Add(message);
        DisplayMessagesInUI();
    }

    public List<MessageData> GetMessages()
    {
        return messageLog;
    }

    public void DisplayMessagesInUI()
    {
        logText.text = "asdasdasdasd";
        string lastNPCTalking = "";

        foreach (var message in messageLog)
        {
            if (lastNPCTalking == message.Name)
            {
                logText.text += $" {message.Message}";
            }
            else { 
            logText.text += $"{message.Name}: {message.Message}\n";
            }

            lastNPCTalking = message.Name;

        }
    }

    public void ClearLog()
    {
        messageLog.Clear();
    }
}
