using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaInteractuable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    public float openAngle = 90f; // Ángulo de apertura de la puerta
    public float openSpeed = 2f; // Velocidad de apertura de la puerta

    private Quaternion closedRotation; // Rotación de la puerta cerrada
    private Quaternion openRotation;
    private bool isOpening = false;
    private bool isClosing = false;
    private bool isClosed = true; // Inicialmente la puerta está cerrada

    [SerializeField] private AudioManager audioManager;

    void Start()
    {
        closedRotation = transform.rotation; // Guarda la rotación inicial de la puerta
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation; // Calcula la rotación abierta
    }

    void Update()
    {
        // Si la puerta está abriéndose, interpola la rotación hacia la rotación abierta
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, openSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, openRotation) < 0.1f)
            {
                transform.rotation = openRotation;
                isOpening = false; // La puerta está completamente abierta
                isClosed = false;

                
            }
        }
        // Si la puerta se está cerrando, interpola la rotación hacia la rotación cerrada
        else if (isClosing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, openSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, closedRotation) < 0.1f)
            {
                transform.rotation = closedRotation;
                isClosing = false;
                isClosed = true; // La puerta está completamente cerrada
                
            }
        }
    }

    // Método para interactuar con la puerta
    public void Interact()
    {
        if (isClosed)
        {
            OpenDoor();
        }
        else //if (isOpen)
        {
            CloseDoor();
        }
    }

    // Método para obtener el texto de interacción
    public string GetInteractText()
    {
        return interactText;
    }

    // Método para obtener el transform
    public Transform GetTransform()
    {
        return transform;
    }

    // Método para abrir la puerta
    public void OpenDoor()
    {
        if(!isOpening && isClosed)
        {
            isOpening = true; 
            isClosing = false;

            PlayDoorSound();
        }
    }

    // Método para cerrar la puerta
    public void CloseDoor()
    {
        if(!isClosing && !isClosed)
        {
            isClosing = true;
            isOpening = false;
        }
    }

    private void PlayDoorSound()
    {
        audioManager.PlaySFX(audioManager.doorOpening);
    }
}
