using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaInteractuable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    public float openAngle = 90f; 
    public float openSpeed = 2f; 

    private Quaternion closedRotation; 
    private Quaternion openRotation;
    private bool isOpening = false;
    private bool isClosed = true;

    void Start()
    {
        closedRotation = transform.rotation; // Guarda la rotación inicial de la puerta
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation; // Calcula la rotación abierta
    }

    void Update()
    {
        if (isOpening && transform.rotation != openRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, openSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, openRotation) < 0.1f)
            {
                transform.rotation = openRotation;
                isOpening = false; // La puerta está completamente abierta
            }
        }
        else if (!isOpening && !isClosed && transform.rotation != closedRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, openSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, closedRotation) < 0.1f)
            {
                transform.rotation = closedRotation;
                isClosed = true; // La puerta está completamente cerrada
            }
        }
    }

    public void Interact()
    {
        if (isClosed)
        {
            OpenDoor();
        }
        else
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

    // Método para abrir la puerta
    public void OpenDoor()
    {
        isOpening = true; 
        isClosed = false; 
    }

    // Método para cerrar la puerta
    public void CloseDoor()
    {
        isOpening = false; // Marca la puerta como no abriéndose
    }
}
