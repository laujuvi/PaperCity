using System.Collections;
using TMPro;
using UnityEngine;

public enum Emotions { Neutral, Talking, Thinking };

public class BoxMessageManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TextMeshProUGUI nameTextMeshPro;

    [SerializeField] float letterDelay = 0.05f;

    private string nameSpeaker;

    public void SendMessage(string name, Color color, string message, Emotions emotion)
    {
        string formattedMessage = FormatMessage(name, color, message, emotion);
        StartCoroutine(DisplayMessage(formattedMessage));
    }

    private string FormatMessage(string name, Color color, string message, Emotions emotion)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(color);
        nameSpeaker = $" <#{hexColor}> {name}";
        return $"[{emotion}] \"{message}\"";
    }

    private IEnumerator DisplayMessage(string formattedMessage)
    {

        nameTextMeshPro.text = nameSpeaker;
        textMeshPro.text = "";
        foreach (char c in formattedMessage)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
