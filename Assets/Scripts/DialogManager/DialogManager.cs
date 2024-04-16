using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EvidenceData
{
    public bool hasEvidence;
    public string evidenceName;
    public string requiredMessage;
}

[System.Serializable]
public class DialogMessage
{
    public bool talked;
    public EvidenceData evidence;
    public string message;
    public string emotion;
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
    private Dictionary<string, bool> evidenceStatus = new Dictionary<string, bool>();

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

        // Agregar los objetos al diccionario evidenceStatus
        foreach (GameObject evidenceObject in evidenceArray)
        {
            evidenceStatus.Add(evidenceObject.name, false);
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
                        boxMessageManager.SendMessage(dialog.name, dialog.color, message.message, (Emotions)System.Enum.Parse(typeof(Emotions), message.emotion));
                        message.talked = true;
                        return;
                    }
                    else
                    {
                        foreach (GameObject evidenceObject in evidenceArray)
                        {
                            if (evidenceObject.name == message.evidence.evidenceName && GetEvidenceStatus(evidenceObject.name) )
                            {
                                boxMessageManager.SendMessage(dialog.name, dialog.color, message.message, (Emotions)System.Enum.Parse(typeof(Emotions), message.emotion));
                                message.talked = true;
                                return;
                            } else
                            {
                                boxMessageManager.SendMessage(dialog.name, dialog.color, message.evidence.requiredMessage, (Emotions)System.Enum.Parse(typeof(Emotions), message.emotion));
                                return;
                            }
                        }

                        // El objeto no existe en evidenceArray
                        Debug.Log("No se encontró la evidencia: " + message.evidence.evidenceName);
                    }
                }

            }
        }

        Debug.LogWarning("Dialog not found or all messages require evidence.");
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

    public void SetEvidenceStatus(string evidenceName, bool status)
    {
        if (evidenceStatus.ContainsKey(evidenceName))
        {
            evidenceStatus[evidenceName] = status;
        }
        else
        {
            Debug.LogError("Evidence with name " + evidenceName + " not found in evidenceArray.");
        }
    }

    public bool GetEvidenceStatus(string evidenceName)
    {
        if (evidenceStatus.ContainsKey(evidenceName))
        {
            return evidenceStatus[evidenceName];
        }
        else
        {
            Debug.LogError("Evidence with name " + evidenceName + " not found in evidenceArray.");
            return false;
        }
    }
}
