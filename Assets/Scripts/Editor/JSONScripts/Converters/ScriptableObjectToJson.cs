using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;

public class ScriptableObjectToJson : MonoBehaviour
{
    [Header("Scriptable Object Input")]
    [SerializeField] private ScriptableObject scriptableObject;

    [Header("Output JSON")]
    private string extensionFile = ".json";
    [SerializeField] private string outputFilePath = "Scripts/Editor/JSONScripts/JSONs/";

    public void ConvertScriptableObjectToJSON()
    {
        if (scriptableObject == null)
        {
            Debug.LogError("No ScriptableObject assigned.");
            return;
        }

        string json = ConvertToJson(scriptableObject);

        SaveJsonToFile(json, outputFilePath);
    }
    private string ConvertToJson(ScriptableObject obj)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Ignoro las referencias ciclicas para que no me de error cuando se convierte el campo "color"
            Formatting = Formatting.Indented,
            ContractResolver = new CustomContractResolver()
        };

        // Serializa el ScriptableObject
        string json = JsonConvert.SerializeObject(obj, settings);

        return json;
    }

    private void SaveJsonToFile(string json, string filePath)
    {
        string path = Path.Combine("Assets", $"{filePath}JSON_{scriptableObject.name}{extensionFile}");

        try
        {
            File.WriteAllText(path, json);
            Debug.Log($"JSON saved at: {path}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error saving JSON_{scriptableObject.name}: " + ex.Message);
        }
    }

    // Contrato personalizado para excluir propiedades específicas
    private class CustomContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            // Excluye las propiedades "hideFlags" que se crea por la libreria de Newtonsoft 
            properties = properties.Where(p => p.PropertyName != "hideFlags").ToList();

            return properties;
        }
    }
}
