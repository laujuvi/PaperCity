using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOpenDoorMessage : MonoBehaviour
{
    [SerializeField] private PruebaInteractuable door;
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private string doorMessage;
    void Start()
    {
        if (door != null)
        {
            door.OnDoorOpening += HandleDoorOpening;
        }
    }

    private void HandleDoorOpening()
    {
        boxMessageManager.SendMessage("Detective", Color.white, doorMessage, Emotions.None);
        door.OnDoorOpening -= HandleDoorOpening;
    }
}
