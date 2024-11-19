using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialog : MonoBehaviour
{
    [SerializeField] private BoxMessageManager boxMessageManager;

    /* INITIAL DIALOGS */
    [SerializeField] string PJName;
    [SerializeField] string[] lines;

    void Start()
    {
        StartDialogue();
    }

    void StartDialogue()
    {
        foreach (string line in lines)
        {
            boxMessageManager.SendMessage(PJName, Color.white, line, Emotions.None);
        }
    }
}
