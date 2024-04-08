using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string interactText;
    private BoxMessageManager boxMessageManager;

    private bool isDialog2 = false;
    private bool isDialog3 = false;
    private bool isDialog4 = false;
    private int interects = 0;

    public void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();

        if (boxMessageManager == null)
        {
            Debug.LogError("No se encontró un BoxMessageManager en la escena.");
            return;
        }
    }


    public void Interact()
    {
        TestMessages();
        Debug.Log("INTERACTUASTE");
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    // ESTE METODO ES A MODO PRUEBA
    private void TestMessages()
    {
        if (interects <= 1)
        {
            boxMessageManager.SendMessage("NPC", Color.blue, "Hello, player!", Emotions.Neutral);
            interects++;
            
        }
        else if (!isDialog2)
        {
            boxMessageManager.SendMessage("NPC", Color.blue, "Why do you keep bothering me?", Emotions.Neutral);
            isDialog2 = true;
        }
        else if (!isDialog3)
        {
            boxMessageManager.SendMessage("NPC", Color.blue, "I'm getting really annoyed now.", Emotions.Angry);
            isDialog3 = true;
        }
        else if (!isDialog4)
        {
            boxMessageManager.SendMessage("NPC", Color.blue, "That's it, I'm done talking to you.", Emotions.Angry);
            isDialog4 = true;
        }
    }
}
