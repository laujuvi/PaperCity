using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PruebaInteractuable_2 : MonoBehaviour, IInteractable
{
    
    public Transform closedPosition;
    public Transform openPosition;
    public float movementSpeed = 5f;

    private Vector3 initialPosition;
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = closedPosition.position;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowards(targetPosition);
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Si llegamos al destino, detenemos el movimiento
        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    public void OpenDrawer()
    {
        if (!isMoving)
        {
            isMoving = true;
            targetPosition = openPosition.position;
        }
    }

    public void CloseDrawer()
    {
        if (!isMoving)
        {
            isMoving = true;
            targetPosition = closedPosition.position;
        }
    }

    public void ResetPosition()
    {
        if (!isMoving)
        {
            isMoving = true;
            targetPosition = initialPosition;
        }
    }

    [SerializeField] private string interactText;
    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact()
    {
        // Aquí puedes definir la lógica para la interacción
        if (transform.position == closedPosition.position)
        {
            OpenDrawer();
        }
        if (transform.position == openPosition.position)
        {
            
            CloseDrawer();
        }
    }

}
