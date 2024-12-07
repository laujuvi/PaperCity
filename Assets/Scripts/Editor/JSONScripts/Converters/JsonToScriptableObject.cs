using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor;

public class JsonToScriptableObject : MonoBehaviour
{
    [Header("JSON Input")]
    [SerializeField] private TextAsset jsonFile;

    [Header("Output ScriptableObject")]
    private string extensionFile = ".asset";
    [SerializeField] private string outputFilePath = "Scripts/Editor/JSONScripts/Scriptables/";

    public void ConverJsonToScriptableObject()
    {
        if (jsonFile == null)
        {
            Debug.LogError("No JSON file assigned.");
            return;
        }

        DialoguesData newScriptableObject = CreateScriptableObjectFromJson(jsonFile.text);

        SaveScriptableObject(newScriptableObject, outputFilePath);
    }

    private DialoguesData CreateScriptableObjectFromJson(string json)
    {
        // Deserializa el JSON en un objeto intermedio
        var intermediateData = JsonConvert.DeserializeObject<DialoguesDataWrapper>(json);

        // Crea una nueva instancia del ScriptableObject
        DialoguesData newSO = ScriptableObject.CreateInstance<DialoguesData>();

        // Copia los datos del objeto intermedio al ScriptableObject
        newSO.dialogues = intermediateData.dialogues;

        Debug.Log($"ScriptableObject creado exitosamente a partir del JSON: {jsonFile.name}");
        return newSO;
    }

    private void SaveScriptableObject(ScriptableObject obj, string filePath)
    {
        try
        {
            string path = Path.Combine("Assets", $"{filePath}SO_{jsonFile.name}{extensionFile}");

            // Guarda el ScriptableObject como un archivo .asset en el proyecto
            AssetDatabase.CreateAsset(obj, path);
            Debug.Log($"ScriptableObject guardado en: {path}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error saving SO_{jsonFile.name}: " + ex.Message);
        }
    }

    // Clase intermedia para deserialización
    [System.Serializable]
    private class DialoguesDataWrapper
    {
        public System.Collections.Generic.List<DialogData> dialogues;
    }
}
