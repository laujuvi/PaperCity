using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataCollected
{
    public int cluesObtained;
    public int minClues;
    public int maxClues;
    public bool hasTalkedAllNPCs = false;
}

public class DataCollector : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager;

    private DataCollected dataCollected;

    private int cluesObtained;
    private int minClues;
    private int maxClues;

    private bool hasTalkedAllNPCs = false;

    private List<GameObject> npcAlreadyInteracted = new List<GameObject>();

    private void Start()
    {
        dataCollected = new DataCollected();
        dialogManager.OnLoopDialog += CheckNPCMaxTalked;
    }

    public void SetAllData()
    {
        SetCluesNumbers();
        SetHasTalkedAllNPCs();
    }

    public DataCollected GetAllData()
    {
        dataCollected.maxClues = maxClues;
        dataCollected.minClues = minClues;
        dataCollected.cluesObtained = cluesObtained;
        dataCollected.hasTalkedAllNPCs = hasTalkedAllNPCs;

        return dataCollected;
    }

    private void SetHasTalkedAllNPCs()
    {
        if (npcAlreadyInteracted.Count == GameManager.Instance.npcInteracted.Count)
        {
            hasTalkedAllNPCs = true;
        }

    }

    private void SetCluesNumbers()
    {
        maxClues = GameManager.Instance.maxEvidence;
        minClues = GameManager.Instance.minEvidence;
        cluesObtained = GameManager.Instance.currentEvidence;
    }

    // Se fija con que npm ya agoto los dialogos de la fase 1
    private void CheckNPCMaxTalked()
    {
        // si no estoy en la fase 1 no reviso nada
        if (!dialogManager.isDialoguesPhase1) return;

        string npcName = dialogManager.currentNPCName;

        foreach (var npc in npcAlreadyInteracted)
        {
            if (npc.name == npcName)
            {
                return; //Si ya hable con este hago return
            }
        }

        // Busco el NPC en la lista del gameManager
        foreach (var npc in GameManager.Instance.npcInteracted)
        {
            if (npc.name == npcName)
            {
                npcAlreadyInteracted.Add(npc); // Lo meto a la lista de interacted
                return;
            }
        }

    }

}
