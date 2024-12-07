using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public Slider mouseSensitivitySlider;

    public delegate void SensitivityChangedHandler();
    public static event SensitivityChangedHandler OnSensitivityChanged;

    private bool isPaused = false;

    private void Start()
    {
        //GameManager.Instance.uIManager.UpdateSensitivitySlider(mouseSensitivitySlider);

        mouseSensitivitySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        //GameManager.Instance.uIManager.SetMenuUIVisibility(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isPaused)
            {
                ResumeGame();
                CursorManager.HideCursor();
            }
            else
            {
                PauseGame();
                CursorManager.ShowCursor();
            }
    }
    private void OnSliderValueChanged()
    {
        OnSensitivityChanged?.Invoke();
    }
    public void ResumeGame()
    {
        GameManager.Instance.uIManager.SetMenuUIVisibility(false);
        GameManager.Instance.uIManager.SetLenIconVisibility(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        GameManager.Instance.uIManager.SetMenuUIVisibility(true);
        GameManager.Instance.uIManager.SetLenIconVisibility(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void GoToOptions()
    {
        GameManager.Instance.uIManager.SetMenuUIVisibility(false);
        GameManager.Instance.uIManager.SetOptionsVisibility(true);
    }
    public void GoBackToMenu()
    {
        GameManager.Instance.uIManager.SetMenuUIVisibility(true);
        GameManager.Instance.uIManager.SetOptionsVisibility(false);
        GameManager.Instance.uIManager.SetControlsVisibility(false);
    }
    public void GoToControls()
    {
        GameManager.Instance.uIManager.SetControlsVisibility(true);
        GameManager.Instance.uIManager.SetMenuUIVisibility(false);
    }
    public void GoToMenu()
    {
        GameManager.Instance.uIManager.OnDestroyGameSettingsGameobject();
        Time.timeScale = 1f;
        GameManager.Instance.uIManager.LoadNextScene();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}