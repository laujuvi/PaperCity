using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

[Serializable]
public class SettingsData
{
    public float mouseSensitivity = 400f;
}
public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    [Header("Sensibilidad")]
    public float mouseSensitivity = 400f;
    public Slider sliderSensitivity;

    [SerializeField] string settingsFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        settingsFilePath = Path.Combine(Application.persistentDataPath, "game_settings.json");

        LoadSensitivity();
        if (sliderSensitivity != null)
        {
            sliderSensitivity.value = mouseSensitivity;
            sliderSensitivity.onValueChanged.AddListener(UpdateSensitivity);
        }
    }
    private void LoadSensitivity()
    {
        if(File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            SettingsData settingsData = JsonUtility.FromJson<SettingsData>(json);
            mouseSensitivity = settingsData.mouseSensitivity;
            Debug.Log("Sensibilidad cargada desde el json");
        }
        else
        {
            mouseSensitivity = 400f;
            SaveSensitivity();
            Debug.Log("Archivo json no encontrado, creando archivo con valores por defecto");
        }

        if (sliderSensitivity != null)
        {
            sliderSensitivity.value = mouseSensitivity;
        }
    }

    private void UpdateSensitivity(float value)
    {
        mouseSensitivity = value;
        SaveSensitivity();
    }

    public void SaveSensitivity()
    {
        SettingsData settingsData = new SettingsData
        {
            mouseSensitivity = mouseSensitivity
        };

        string json = JsonUtility.ToJson(settingsData, true);
        File.WriteAllText(settingsFilePath, json);
        Debug.Log("Archivo Guardado");
    }
    public void UpdateSensitivitySlider(Slider newSlider)
    {
        if (sliderSensitivity != null)
        {
            sliderSensitivity.onValueChanged.RemoveListener(UpdateSensitivity);
        }

        sliderSensitivity = newSlider;

        sliderSensitivity.value = mouseSensitivity;

        sliderSensitivity.onValueChanged.AddListener(UpdateSensitivity);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}