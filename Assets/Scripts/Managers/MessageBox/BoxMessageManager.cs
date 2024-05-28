using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxMessageManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TextMeshProUGUI nameTextMeshPro;
    [SerializeField] GameObject bgDialog;

    [SerializeField] float letterDelay = 0.15f;
    [SerializeField] float hideDialogDelay = 1f;

    private bool isDisplayingMessage = false;
    private bool isSkippingDialog = false;
    private bool interruptWait = false;
    private Queue<MessageData> messageQueue = new Queue<MessageData>();

    public bool IsDisplayingMessage()
    {
        return isDisplayingMessage;
    }
    public bool IsSkippingDialog()
    {
        return isSkippingDialog;
    }

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

            float elapsedTime = 0f;
            while (elapsedTime < hideDialogDelay && !interruptWait)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            textMeshPro.text = "";
            nameTextMeshPro.text = "";
        }

        isDisplayingMessage = false;
        ResetSkippingTimers();
        bgDialog.SetActive(false);
    }

    private string FormatMessage(string name, Color color, string message, Emotions emotion)
    {
        //string hexColor = ColorUtility.ToHtmlStringRGB(color);
        //nameSpeaker = $" <#{hexColor}> {name}";
        nameTextMeshPro.color = color;
        //return $"[{emotion}] \"{message}\"";
        return $" \"{message}\"";
    }

    public void CheckSkipDialog()
    {
        if (!isSkippingDialog) { 
            SpeedUpDialog();
        }
        else
        {
            SkipDialog();
        }
    }
    private void SpeedUpDialog()
    {
        isSkippingDialog = true;
        letterDelay = 0.01f;
        hideDialogDelay = 100f;
    }

    private void SkipDialog()
    {
        interruptWait = true;
        isSkippingDialog = false;
    }

    private void ResetSkippingTimers()
    {
        letterDelay = 0.05f;
        hideDialogDelay = 1f;
        isSkippingDialog = false;
        interruptWait = false;
    }
}
