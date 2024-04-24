using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaInteractuable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    public float openAngle = 90f; // �ngulo de apertura de la puerta
    public float openSpeed = 2f; // Velocidad de apertura de la puerta

    private Quaternion closedRotation; // Rotaci�n de la puerta cerrada
    private Quaternion openRotation; // Rotaci�n de la puerta abierta
    private bool isOpening = false; // Variable para controlar si la puerta est� abri�ndose
    public void Interact()
    {
        if (!isOpening)
        {
            OpenDoor();
        }
        else if (isOpening)
        {
            CloseDoor();
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void Start()
    {
        closedRotation = transform.rotation; // Guarda la rotaci�n inicial de la puerta
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation; // Calcula la rotaci�n abierta
    }

    private void Update()
    {
        // Si la puerta est� abri�ndose, interpola la rotaci�n hacia la rotaci�n abierta
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, openSpeed * Time.deltaTime);
        }
    }

    // M�todo para abrir la puerta
    public void OpenDoor()
    {
        isOpening = true; // Marca la puerta como abri�ndose
    }

    // M�todo para cerrar la puerta
    public void CloseDoor()
    {
        isOpening = false; // Marca la puerta como no abri�ndose
        transform.rotation = closedRotation; // Vuelve a la rotaci�n cerrada
    }
}
