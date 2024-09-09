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
}
