using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInitialDialog : MonoBehaviour
{
    [SerializeField] private BoxMessageManager boxMessageManager;

    /* INITIAL DIALOGS */
    [SerializeField] string PJName;
    [SerializeField] string[] lines;
    [SerializeField] string[] lines2;

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

    public void SecondDialogue()
    {
        foreach (string line in lines2)
        {
            boxMessageManager.SendMessage(PJName, Color.white, line, Emotions.None);
        }
    }

}
