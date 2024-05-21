using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private BoxMessageManager boxMessageManager;

    void Start()
    {
        boxMessageManager = FindObjectOfType<BoxMessageManager>();

        if (boxMessageManager == null)
        {
            Debug.LogError("No se encontró un BoxMessageManager en la escena.");
            return;
        }

        boxMessageManager.SendMessage("Player", Color.blue, "Hello, world!", Emotions.Talking);
    }
}
