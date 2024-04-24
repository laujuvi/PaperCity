using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaInteractuable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    public float openAngle = 90f; // Ángulo de apertura de la puerta
    public float openSpeed = 2f; // Velocidad de apertura de la puerta

    private Quaternion closedRotation; // Rotación de la puerta cerrada
    private Quaternion openRotation; // Rotación de la puerta abierta
    private bool isOpening = false; // Variable para controlar si la puerta está abriéndose
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
        closedRotation = transform.rotation; // Guarda la rotación inicial de la puerta
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation; // Calcula la rotación abierta
    }

    private void Update()
    {
        // Si la puerta está abriéndose, interpola la rotación hacia la rotación abierta
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, openSpeed * Time.deltaTime);
        }
    }

    // Método para abrir la puerta
    public void OpenDoor()
    {
        isOpening = true; // Marca la puerta como abriéndose
    }

    // Método para cerrar la puerta
    public void CloseDoor()
    {
        isOpening = false; // Marca la puerta como no abriéndose
        transform.rotation = closedRotation; // Vuelve a la rotación cerrada
    }
}
