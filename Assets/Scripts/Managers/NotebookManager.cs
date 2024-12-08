using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] private GameObject notebook;
    [SerializeField] private GameObject notebookIcon;

    private bool isNotebookWaitingForMessage = false;
    private bool isIconManualyDisabled = false;

    private void Update()
    {
        // Ocultar el icono si se está mostrando un mensaje o la libreta está activa
        if (GameManager.Instance.isDisplayingMessage || notebook.activeSelf)
        {
            notebookIcon.SetActive(false);

            // Si la libreta esta activa mientras se muestra un mensaje, se cierra temporalmente
            if (GameManager.Instance.isDisplayingMessage && notebook.activeSelf)
            {
                notebook.SetActive(false);
                isNotebookWaitingForMessage = true; // Marcar que debe reabrirse al finalizar el mensaje
            }
        }
        else
        {
            // mostrar el icono si no hay mensajes y la libreta no esta activa
            if (!isIconManualyDisabled) notebookIcon.SetActive(true);

            // Reabrir la libreta si estaba esperando a que termine el mensaje
            if (isNotebookWaitingForMessage)
            {
                notebook.SetActive(true);
                isNotebookWaitingForMessage = false; // Restablece estado
            }
        }
    }

    // Metodos para controlar manualmente la libreta y el icono
    public void DisableNotebook()
    {
        notebook.SetActive(false);
        isNotebookWaitingForMessage = false;
    }

    public void EnableNotebook()
    {
        notebook.SetActive(true);
        isNotebookWaitingForMessage = false; // Asegurar que no quede en espera
    }

    public void DisableNotebookIcon()
    {
        isIconManualyDisabled = true;
        notebookIcon.SetActive(false);
    }

    public void EnableNotebookIcon()
    {
        isIconManualyDisabled = false;
        notebookIcon.SetActive(true);
    }

    public bool NotebookStatus()
    {
        return notebook.activeSelf;
    }
}
