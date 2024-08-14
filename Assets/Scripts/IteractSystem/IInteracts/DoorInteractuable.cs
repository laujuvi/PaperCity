using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaInteractuable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    public float openAngle = 90f; // �ngulo de apertura de la puerta
    public float openSpeed = 2f; // Velocidad de apertura de la puerta

    private Quaternion closedRotation; // Rotaci�n de la puerta cerrada
    private Quaternion openRotation;
    private bool isOpening = false;
    private bool isClosing = false;
    private bool isClosed = true; // Inicialmente la puerta est� cerrada

    [SerializeField] private AudioManager audioManager;

    void Start()
    {
        closedRotation = transform.rotation; // Guarda la rotaci�n inicial de la puerta
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation; // Calcula la rotaci�n abierta
    }

    void Update()
    {
        // Si la puerta est� abri�ndose, interpola la rotaci�n hacia la rotaci�n abierta
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, openSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, openRotation) < 0.1f)
            {
                transform.rotation = openRotation;
                isOpening = false; // La puerta est� completamente abierta
                isClosed = false;

                
            }
        }
        // Si la puerta se est� cerrando, interpola la rotaci�n hacia la rotaci�n cerrada
        else if (isClosing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, openSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, closedRotation) < 0.1f)
            {
                transform.rotation = closedRotation;
                isClosing = false;
                isClosed = true; // La puerta est� completamente cerrada
                
            }
        }
    }

    // M�todo para interactuar con la puerta
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

    // M�todo para obtener el texto de interacci�n
    public string GetInteractText()
    {
        return interactText;
    }

    // M�todo para obtener el transform
    public Transform GetTransform()
    {
        return transform;
    }

    // M�todo para abrir la puerta
    public void OpenDoor()
    {
        if(!isOpening && isClosed)
        {
            isOpening = true; 
            isClosing = false;

            PlayDoorSound();
        }
    }

    // M�todo para cerrar la puerta
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
