using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectValidator : MonoBehaviour
{
    [Header("ScriptableObject Input")]
    [SerializeField] private DialoguesData dialoguesData;

    public void ValidateScriptableObject()
    {
        if (dialoguesData == null)
        {
            Debug.LogError("No ScriptableObject assigned.");
            return;
        }

        ValidateAndModifyScriptableObject(dialoguesData);

    }

    private void ValidateAndModifyScriptableObject(DialoguesData data)
    {
        foreach (var dialogue in data.dialogues)
        {
            // Validar que "name" no este vacio
            if (string.IsNullOrEmpty(dialogue.name))
            {
                Debug.LogError($"Validation Error: 'name' cannot be empty in dialogue.");
                continue;
            }

            foreach (var message in dialogue.messages)
            {
                // Valida que "message" no este vacio
                if (string.IsNullOrEmpty(message.message))
                {
                    Debug.LogError($"Validation Error: 'message' cannot be empty in dialogue '{dialogue.name}'.");
                    continue;
                }

                // Valida que "emotion" no este vacio
                if (string.IsNullOrEmpty(message.emotion))
                {
                    Debug.LogError($"Validation Error: 'emotion' cannot be empty in dialogue '{dialogue.name}'.");
                    continue;
                }

                // Asegura de que "talked" sea siempre falso
                message.talked = false;

                // Validar los requisitos de evidencia
                if (message.evidence.hasEvidence)
                {
                    // Si "hasEvidence" esta en true entonces "evidenceName" y "requiredMessage" no deben estar vacios
                    if (string.IsNullOrEmpty(message.evidence.evidenceName) ||
                        string.IsNullOrEmpty(message.evidence.requiredMessage))
                    {
                        Debug.LogError($"Validation Error: 'evidenceName' and 'requiredMessage' must have content if 'hasEvidence' is true in dialogue '{dialogue.name}'.");
                        continue;
                    }
                }
            }
        }

        Debug.Log("ScriptableObject validation and modification completed.");
    }
}
