using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject lenIcon;
    public GameObject options;
    public GameObject controls;
    public Slider mouseSensitivitySlider;

    public delegate void SensitivityChangedHandler();
    public static event SensitivityChangedHandler OnSensitivityChanged;

    [SerializeField] private SceneLoadManager sceneLoadManager;
    private bool isPaused = false;
    private void Start()
    {
        if(GameSettings.Instance != null)
        {
            GameSettings.Instance.UpdateSensitivitySlider(mouseSensitivitySlider);

            mouseSensitivitySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        }
        menuUI.SetActive(false);
    }
    private void Update()
    {
        //Debug.Log(GameSettings.Instance.mouseSensitivity);
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isPaused)
            {
                ResumeGame();
                GameManager.Instance.HideCursor();
            }
            else if (!isPaused)
            {
                PauseGame();
                GameManager.Instance.ShowCursor();
            }
    }
    private void OnSliderValueChanged()
    {
        OnSensitivityChanged?.Invoke();
    }
    public void ResumeGame()
    {
        menuUI.SetActive(false);
        lenIcon.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        menuUI.SetActive(true);
        lenIcon.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void GoToOptions()
    {
        menuUI.SetActive(false);
        options.SetActive(true);
        print("options");
    }
    public void GoBackToMenu()
    {
        menuUI.SetActive(true);
        options.SetActive(false);
        controls.SetActive(false);
        print("menu");
    }
    public void GoToControls()
    {
        controls.SetActive(true);
        menuUI.SetActive(false);
    }
    public void GoToMenu()
    {
        GameSettings existingGameSettings = FindObjectOfType<GameSettings>();
        if (existingGameSettings != null)
        {
            Destroy(existingGameSettings.gameObject);
        }
        Time.timeScale = 1f;
        sceneLoadManager.LoadNextScene();
    }
    public void QuitGame()
    {
        Application.Quit();
        print("quit");
    }
}
