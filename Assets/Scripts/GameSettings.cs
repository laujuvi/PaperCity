using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }
    public float mouseSensitivity = 400f;
    public Slider sliderSensitivity;
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
        }
        LoadSensitivity();
        sliderSensitivity.onValueChanged.AddListener(UpdateSensitivity);
    }
    private void LoadSensitivity()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("CurrentSensitivity", 1f);
        sliderSensitivity.value = mouseSensitivity;
    }

    private void UpdateSensitivity(float value)
    {
        mouseSensitivity = value;
        SaveSensitivity();
    }

    public void SaveSensitivity()
    {
        PlayerPrefs.SetFloat("CurrentSensitivity", mouseSensitivity);
    }
}