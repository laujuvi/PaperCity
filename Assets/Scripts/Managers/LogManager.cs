using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    private List<MessageData> messageLog = new List<MessageData>();

    public void AddMessage(MessageData message)
    {
        messageLog.Add(message);
    }

    public List<MessageData> GetMessages()
    {
        return messageLog;
    }

    public void DisplayMessagesInUI(TextMeshProUGUI logText)
    {
        logText.text = ""; 

        foreach (var message in messageLog)
        {
            logText.text += $"{message.Name}: {message.Message}\n";
        }
    }

    public void ClearLog()
    {
        messageLog.Clear();
    }
}
