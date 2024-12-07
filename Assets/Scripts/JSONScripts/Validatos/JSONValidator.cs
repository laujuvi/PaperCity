using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JSONValidator : MonoBehaviour
{
    [Header("JSON Input")]
    [SerializeField] private TextAsset dialogJSON;

    public void ValidateJSON()
    {

        if (dialogJSON == null)
        {
            Debug.LogError("No JSON assigned.");
            return;
        }
        #if UNITY_EDITOR
        string originalPath = GetOriginalJsonPath(dialogJSON);
        #endif
        var modifiedData = ValidateAndModifyJSON(dialogJSON.text);

        string modifiedJson = JsonConvert.SerializeObject(modifiedData, Formatting.Indented);

        #if UNITY_EDITOR
        SaveModifiedJSON(modifiedJson, originalPath);
        #endif
    }

    private JObject ValidateAndModifyJSON(string json)
    {
        JObject parsedData = JObject.Parse(json);

        foreach (var dialogue in parsedData["dialogues"])
        {
            // Validar que "name" no este vacio
            string name = dialogue["name"]?.ToString();
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError($"Validation Error: 'name' cannot be empty. Found at line {((IJsonLineInfo)dialogue).LineNumber}.");
                continue;
            }

            foreach (var message in dialogue["messages"])
            {
                // Valida que "message" no este vacio
                string messageText = message["message"]?.ToString();
                if (string.IsNullOrEmpty(messageText))
                {
                    Debug.LogError($"Validation Error: 'message' cannot be empty in dialog '{name}'.  Found at line {((IJsonLineInfo)dialogue).LineNumber}");
                    continue;
                }

                // Si "isLoopingMessage" no fue seteado lo dejo en false
                if (message["isLoopingMessage"] == null)
                {
                    message["isLoopingMessage"] = false;
                    Debug.Log($"Added 'isLoopingMessage' with value false in dialog '{name}'. Found at line {((IJsonLineInfo)dialogue).LineNumber}");
                }

                // Valida que "emotion" no este vacio
                string emotion = message["emotion"]?.ToString();
                if (string.IsNullOrEmpty(emotion))
                {
                    Debug.LogError($"Validation Error: 'emotion' cannot be empty in dialog '{name}'. Found at line {((IJsonLineInfo)dialogue).LineNumber}");
                    continue;
                }

                // Asegura de que "talked" sea siempre falso
                message["talked"] = false;

                // Validar los requisitos de evidencia
                if (message["evidence"]?["hasEvidence"]?.ToObject<bool>() == true)
                {
                    // Si "hasEvidence" esta en true entonces "evidenceName" y "requiredMessage" no deben estar vacios
                    if (string.IsNullOrEmpty(message["evidence"]["evidenceName"]?.ToString()) ||
                        string.IsNullOrEmpty(message["evidence"]["requiredMessage"]?.ToString()))
                    {
                        Debug.LogError($"Validation Error: 'evidenceName' and 'requiredMessage' must have content if 'hasEvidence' is true in dialog '{name}'. Found at line {((IJsonLineInfo)dialogue).LineNumber}");
                        continue;
                    }
                }
            }
        }

        return parsedData;
    }

    private void SaveModifiedJSON(string modifiedJson, string path)
    {
        try
        {
            File.WriteAllText(path, modifiedJson);
            Debug.Log("JSON modificado y sobrescrito en: " + path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al sobrescribir el JSON: " + ex.Message);
        }
    }

#if UNITY_EDITOR
    private string GetOriginalJsonPath(TextAsset jsonAsset)
    {

        // Usa AssetDatabase para obtener el path del archivo JSON en el proyecto
        string path = AssetDatabase.GetAssetPath(jsonAsset);

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Path not found in JSON. Using 'Assets/' path");
            return null;
        }

        return Path.Combine(Application.dataPath, path.Replace("Assets/", ""));
    }
#endif
}
