using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialog : MonoBehaviour
{
    [SerializeField] private BoxMessageManager boxMessageManager;

    /* INITIAL DIALOGS */
    [SerializeField] string PJName;
    [SerializeField] string[] lines;

    // Start is called before the first frame update
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
