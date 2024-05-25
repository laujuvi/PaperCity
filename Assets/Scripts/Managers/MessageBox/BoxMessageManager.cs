using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxMessageManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TextMeshProUGUI nameTextMeshPro;
    [SerializeField] GameObject bgDialog;

    [SerializeField] float letterDelay = 0.05f;
    [SerializeField] float hideDialogDelay = 1f;

    private bool isDisplayingMessage = false;
    private Queue<MessageData> messageQueue = new Queue<MessageData>();

    public void SendMessage(string name, Color color, string message, Emotions emotion)
    {
        MessageData data = new MessageData(name, color, message, emotion);
        messageQueue.Enqueue(data);

        if (!isDisplayingMessage)
        {
            bgDialog.SetActive(true);
            StartCoroutine(DisplayMessage());
        }
    }

    private IEnumerator DisplayMessage()
    {
        isDisplayingMessage = true;

        while (messageQueue.Count > 0)
        {
            MessageData data = messageQueue.Dequeue();
            string formattedMessage = FormatMessage(data.Name, data.Color, data.Message, data.Emotion);

            nameTextMeshPro.text = data.Name;
            textMeshPro.text = "";
            foreach (char c in formattedMessage)
            {
                textMeshPro.text += c;
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(hideDialogDelay);
            textMeshPro.text = "";
            nameTextMeshPro.text = "";
        }

        isDisplayingMessage = false;
        bgDialog.SetActive(false);
    }

    private string FormatMessage(string name, Color color, string message, Emotions emotion)
    {
        //string hexColor = ColorUtility.ToHtmlStringRGB(color);
        //nameSpeaker = $" <#{hexColor}> {name}";
        nameTextMeshPro.color = color;
        return $"[{emotion}] \"{message}\"";
    }

    public bool IsDisplayingMessage()
    {
        return isDisplayingMessage;
    }
}
