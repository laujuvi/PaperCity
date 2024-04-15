using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EvidenceData
{
    public bool hasEvidence;
    public string evidenseName;
}

[System.Serializable]
public class DialogMessage
{
    public bool talked;
    public EvidenceData evidence;
    public string message;
    public Emotions emotion;
}

[System.Serializable]
public class DialogData
{
    public string name;
    public Color color;
    public List<DialogMessage> messages;
}

[System.Serializable]
public class DialoguesWrapper
{
    public List<DialogData> dialogues;
}

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextAsset dialoguesJson;
    [SerializeField] private GameObject[] evidenceArray;
    private BoxMessageManager boxMessageManager;

    private DialoguesWrapper dialoguesWrapper;

    private void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();
        if (dialoguesJson != null)
        {
            LoadDialoguesFromJson();
        }
        else
        {
            Debug.LogError("Dialogues JSON not assigned.");
        }
    }

    private void LoadDialoguesFromJson()
    {
        dialoguesWrapper = JsonUtility.FromJson<DialoguesWrapper>(dialoguesJson.ToString());
    }

    public void DisplayDialog(string name)
    {
        DialogData dialog = FindDialogByName(name);

        if (dialog != null)
        {
            foreach (var message in dialog.messages)
            {
                if (!message.talked)
                {
                    if (!message.evidence.hasEvidence)
                    {
                        boxMessageManager.SendMessage(dialog.name, dialog.color, message.message, message.emotion);
                        message.talked = true;
                        return;
                    }
                    else
                    {
                        foreach (GameObject evidenceObject in evidenceArray)
                        {
                            if (evidenceObject.name == message.evidence.evidenseName)
                            {
                                // El objeto existe en evidenceArray
                                Debug.Log("Requiere evidencia: " + message.evidence.evidenseName);
                                return;
                            }
                        }

                        // El objeto no existe en evidenceArray
                        Debug.Log("No se encontró la evidencia: " + message.evidence.evidenseName);
                    }
                }

            }
        }

        Debug.LogError("Dialog not found or all messages require evidence.");
    }

    private DialogData FindDialogByName(string name)
    {
        foreach (var dialog in dialoguesWrapper.dialogues)
        {
            if (dialog.name == name)
            {
                return dialog;
            }
        }

        return null;
    }
}
