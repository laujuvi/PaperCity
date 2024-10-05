using System;
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
    public bool isLoopingMessage;
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
    /*MANAGERS*/
    private BoxMessageManager boxMessageManager;
    private GameManager gameManager;

    /*JSON*/
    [SerializeField] private TextAsset dialoguesPhase1Json;
    [SerializeField] private TextAsset dialoguesPhase2Json;
    [SerializeField] private TextAsset dialoguesPhaseFinalJson;

    private DialoguesWrapper currentDialoguesWrapper;

    private DialoguesWrapper dialoguesPhase1Wrapper;
    private DialoguesWrapper dialoguesPhase2Wrapper;
    private DialoguesWrapper dialoguesPhaseFinalWrapper;

    /*EVIDENCE*/
    [SerializeField] private GameObject[] evidenceArray;
    [SerializeField] private int minEvidenceForPhase2 = 1;
    [SerializeField] private int minEvidenceForPhaseFinal = 2;
    private int totalEvidence;


    private Dictionary<string, bool> evidenceStatus = new Dictionary<string, bool>();

    private void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();
        gameManager = FindObjectOfType<GameManager>();
        

        if (dialoguesPhase1Json != null && dialoguesPhase2Json != null && dialoguesPhaseFinalJson != null)
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
            totalEvidence++;
        }
    }

    private void LoadDialoguesFromJson()
    {
        dialoguesPhase1Wrapper = JsonUtility.FromJson<DialoguesWrapper>(dialoguesPhase1Json.ToString());
        dialoguesPhase2Wrapper = JsonUtility.FromJson<DialoguesWrapper>(dialoguesPhase2Json.ToString());
        dialoguesPhaseFinalWrapper = JsonUtility.FromJson<DialoguesWrapper>(dialoguesPhaseFinalJson.ToString());

        currentDialoguesWrapper = dialoguesPhase1Wrapper;

    }

    public void DisplayDialog(string name)
    {
        int totalCurrentEvidence = GetTrueEvidenceCount();

        if (totalCurrentEvidence >= minEvidenceForPhase2 && totalCurrentEvidence < minEvidenceForPhaseFinal) currentDialoguesWrapper = dialoguesPhase2Wrapper;
        if (totalCurrentEvidence >= minEvidenceForPhaseFinal) currentDialoguesWrapper = dialoguesPhaseFinalWrapper;
        DialogData dialog = FindDialogByName(name, currentDialoguesWrapper);

        if (dialog != null)
        {
            foreach (var message in dialog.messages)
            {
                if (!message.talked)
                {

                    if (message.isLoopingMessage)
                    {
                        boxMessageManager.SendMessage(dialog.name, dialog.color, message.message, (Emotions)System.Enum.Parse(typeof(Emotions), message.emotion));
                        gameManager.SetNPCName(dialog.name);
                        return;
                    }
                    
                    // Si no tiene evidencia que buscar muestro el msj normalmente
                    if (!message.evidence.hasEvidence)
                    {
                        boxMessageManager.SendMessage(dialog.name, dialog.color, message.message, (Emotions)System.Enum.Parse(typeof(Emotions), message.emotion));
                        message.talked = true;
                        gameManager.SetNPCName(dialog.name);
                        return;
                    }

                    // Si hay evidencia pruebo ver que exista en el array de pistas que tengo en DialogManager
                    if (message.evidence.hasEvidence)
                    {
                        foreach (GameObject evidenceObject in evidenceArray)
                        {
                            if (evidenceObject.name == message.evidence.evidenceName)
                            {
                                if (GetEvidenceStatus(evidenceObject.name))
                                {
                                    // Si encuentra la evidencia toma el mensaje como leido y lee el contenido de message
                                    boxMessageManager.SendMessage(dialog.name, dialog.color, message.message, (Emotions)System.Enum.Parse(typeof(Emotions), message.emotion));
                                    message.talked = true;
                                    gameManager.SetNPCName(dialog.name);
                                    return;
                                }
                                // Si no encuentra la evidencia no toma el mensaje como leido y lee el contenido de requireMessage
                                boxMessageManager.SendMessage(dialog.name, dialog.color, message.evidence.requiredMessage, (Emotions)System.Enum.Parse(typeof(Emotions), message.emotion));
                                gameManager.SetNPCName(dialog.name);
                                return;
                            }

                            // El objeto no existe en evidenceArray o no esta en true
                            Debug.LogWarning("La evidencia del JSON con el nombre '" + message.evidence.evidenceName + "' no se encontro en evidenceArray de DialogManager");
                            return;
                        }
                    }
                }

            }
        }

        Debug.LogWarning("Dialogo no encontrado, chequea que el ultimo mensage del JSON contenga 'isLoopingMessage' : true ");
    }

    private DialogData FindDialogByName(string name, DialoguesWrapper jsonDialogues)
    {
        foreach (var dialog in jsonDialogues.dialogues)
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
        Debug.Log("Recibido");
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

    /*Obtiene la cantidad de evidencia que ya recolectamos*/
    public int GetTrueEvidenceCount() 
    {
        int count = 0;
        foreach (var e in evidenceStatus)
        {
            if (e.Value)
            {
                count++;
            }
        }
        return count;
    }

    public Dictionary<string, bool> GetEvidenceList()
    {
        return evidenceStatus;
    }

    public int GetTotalEvidence()
    {
        return totalEvidence;
    }
}
