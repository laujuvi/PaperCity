using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageData
{
    public Emotions emotion;
    public string imageName; 
    public Texture image; 
}

public class BoxMessageManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TextMeshProUGUI nameTextMeshPro;
    [SerializeField] GameObject bgDialog;

    [SerializeField] float letterDelay = 0.15f;
    [SerializeField] float hideDialogDelay = 1f;

    [SerializeField] RawImage rawImage; // RawImage para mostrar la imagen.
    [SerializeField] Texture imageDefault; // Imagen de personajes no importantes.
    [SerializeField] List<ImageData> imageList; // Lista de imágenes con sus nombres.

    private bool isDisplayingMessage = false;
    private bool isSkippingDialog = false;
    private bool interruptWait = false; // Revisar si lo seguimos usando
    private int maxMessageLength = 381; //Aca se edita la cantidad de caracteres que se quieran ver en el cuadro de dialogo
    private Queue<MessageData> messageQueue = new Queue<MessageData>();

    //private void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

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

        SetImage(name);

        Queue<MessageData> splitMessages = SplitMessage(name, color, message, emotion, maxMessageLength);

        foreach (var msgData in splitMessages)
        {
            messageQueue.Enqueue(msgData);
        }

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
            ResetSkippingTimers();
            string formattedMessage = FormatMessage(data.Name, data.Color, data.Message, data.Emotion);

            nameTextMeshPro.text = data.Name;
            ////////////VIEJO////////////
            //textMeshPro.text = "";
            /////////////////////////////

            ////////////NUEVO////////////
            textMeshPro.text = formattedMessage;
            textMeshPro.maxVisibleCharacters = 0;  // Inicia el texto oculto.
            /////////////////////////////

            ////////////VIEJO////////////
            //foreach (char c in formattedMessage)
            //{
            //    textMeshPro.text += c;
            //    if (!isSkippingDialog) yield return new WaitForSeconds(letterDelay);
            //}
            /////////////////////////////

            ////////////NUEVO////////////
            int totalCharacters = textMeshPro.text.Length;
            int visibleCharacters = 0;
            while (visibleCharacters < totalCharacters)
            {
                visibleCharacters++;
                textMeshPro.maxVisibleCharacters = visibleCharacters;
                if (!isSkippingDialog) yield return new WaitForSeconds(letterDelay);
            }
            /////////////////////////////

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
        letterDelay = 0f;
        hideDialogDelay = 1000f;
    }

    private void SkipDialog()
    {
        hideDialogDelay = 0f;
    }

    private void ResetSkippingTimers()
    {
        letterDelay = 0.05f;
        hideDialogDelay = 1f;
        isSkippingDialog = false;
        interruptWait = false;
    }

    private Queue<MessageData> SplitMessage(string name, Color color, string message, Emotions emotion, int maxLength)
    {
        Queue<MessageData> messageParts = new Queue<MessageData>();

        while (message.Length > maxLength)
        {
            int splitIndex = message.LastIndexOf(' ', maxLength);
            if (splitIndex == -1)
            {
                splitIndex = maxLength;
            }
            string part = message.Substring(0, splitIndex).Trim();
            message = message.Substring(splitIndex).Trim();
            messageParts.Enqueue(new MessageData(name, color, part, emotion));
        }

        if (message.Length > 0)
        {
            messageParts.Enqueue(new MessageData(name, color, message, emotion));
        }

        return messageParts;
    }
    private void SetImage(string imageName)
    {
        foreach (var imageData in imageList)
        {
            if (imageData.imageName == imageName)
            {
                rawImage.texture = imageData.image;
                return;
            }
        }
        rawImage.texture = imageDefault;
    }
}
