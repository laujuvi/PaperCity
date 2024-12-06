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
    /* MANAGER */
    [Header("LogManager(opcional)")]
    [SerializeField] private LogManager logManager; // Opcional, en caso de que se quiera usar el Log se puede setear

    [Header("Parametros\n")]
    [SerializeField] float letterDelay = 0.15f;
    [SerializeField] float hideDialogDelay = 1f; 
    private bool isDisplayingMessage = false;
    private bool isSkippingDialog = false;
    private int maxMessageLength = 290; //Aca se edita la cantidad de caracteres que se quieran ver en el cuadro de dialogo
    private Queue<MessageData> messageQueue = new Queue<MessageData>();

    //[Header("UI\n")]

    [Header("Textos")]
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TextMeshProUGUI nameTextMeshPro;

    //[Header("Cuadro de dialogo")]
    //[SerializeField] GameObject bgDialog;

    [Header("Imagenes")]
    [SerializeField] RawImage pjImage; // RawImage para mostrar la imagen del personaje.
    [SerializeField] Texture imageDefault; // Imagen de personajes no importantes.
    [SerializeField] List<ImageData> imageList; // Lista de imágenes con sus nombres.

    [SerializeField] UIManager uiManager;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public bool IsDisplayingMessage()
    {
        return isDisplayingMessage;
    }

    public void SendMessage(string name, Color color, string message, Emotions emotion)
    {

        SetImage(name);

        Queue<MessageData> splitMessages = SplitMessage(name, color, message, emotion, maxMessageLength);

        foreach (var msgData in splitMessages)
        {
            messageQueue.Enqueue(msgData);
            if (logManager != null) logManager.AddMessage(msgData);
        }

        if (!isDisplayingMessage)
        {
            //bgDialog.SetActive(true);
            uiManager.SetBgDialogVisibility(true);
            StartCoroutine(DisplayMessage());
        }
    }

    private IEnumerator DisplayMessage()
    {
        isDisplayingMessage = true;

        uiManager.SetLenIconVisibility(false);

        while (messageQueue.Count > 0)
        {
            MessageData data = messageQueue.Dequeue();
            ResetSkippingTimers();
            //Seteo el mensaje
            FormatMessage(data.Message, data.Emotion);
            // Seteo el nombre
            FormatName(data.Name, data.Color);

            textMeshPro.maxVisibleCharacters = 0;  // Inicia el texto oculto.

            int totalCharacters = textMeshPro.text.Length;
            int visibleCharacters = 0;
            while (visibleCharacters < totalCharacters)
            {
                visibleCharacters++;
                textMeshPro.maxVisibleCharacters = visibleCharacters;
                if (!isSkippingDialog) yield return new WaitForSeconds(letterDelay);
            }

            float elapsedTime = 0f;
            while (elapsedTime < hideDialogDelay)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            textMeshPro.text = "";
            nameTextMeshPro.text = "";
        }

        isDisplayingMessage = false;
        ResetSkippingTimers();
        //bgDialog.SetActive(false);
        uiManager.SetBgDialogVisibility(false);

        uiManager.SetLenIconVisibility(true);
    }

    private void FormatMessage(string message, Emotions emotion)
    {
        textMeshPro.text = message;
    }
    private void FormatName(string name, Color color)
    {
        nameTextMeshPro.color = color;
        nameTextMeshPro.text = name;
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
    }

    private Queue<MessageData> SplitMessage(string name, Color color, string message, Emotions emotion, int maxLength)
    {
        // Inicializo una nueva queue
        Queue<MessageData> messageParts = new Queue<MessageData>();

        // Mientras la longitud del mensaje sea mayor que maxLength sigo dividiendo el mensaje
        while (message.Length > maxLength)
        {
            // Busco el ultimo espacio dentro del limite de caracteres
            int splitIndex = message.LastIndexOf(' ', maxLength);
            // Si no se encuentro un espacio pongo splitIndex como maxLength.
            if (splitIndex == -1)
            {
                splitIndex = maxLength;
            }
            // Elimino espacios al inicio y al final y me quedo con el mensage que procese
            string part = message.Substring(0, splitIndex).Trim();
            // Elimino el fragmento recien procesado del mensaje original
            message = message.Substring(splitIndex).Trim();
            // Me meto en la queue el mensaje procesado
            messageParts.Enqueue(new MessageData(name, color, part, emotion));
        }

        // Si me quedo un mensaje mas chico que el limite lo mando a la queue tambien
        if (message.Length > 0)
        {
            messageParts.Enqueue(new MessageData(name, color, message, emotion));
        }

        return messageParts;
    }

    // Busca los personajes seteados en el editor para ver que foto poner en el dialogo
    private void SetImage(string imageName)
    {
        foreach (var imageData in imageList)
        {
            if (imageData.imageName == imageName)
            {
                pjImage.texture = imageData.image;
                return;
            }
        }
        pjImage.texture = imageDefault;
    }
}
