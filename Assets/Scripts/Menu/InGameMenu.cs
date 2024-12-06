using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public Slider mouseSensitivitySlider;

    public delegate void SensitivityChangedHandler();
    public static event SensitivityChangedHandler OnSensitivityChanged;

    private bool isPaused = false;

    [SerializeField] UIManager uiManager;
    [SerializeField] GameSettings gameSettings;
    private void Start()
    {
        if(gameSettings != null)
        {
            gameSettings.UpdateSensitivitySlider(mouseSensitivitySlider);

            mouseSensitivitySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        }
        uiManager.SetMenuUIVisibility(false);
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
        uiManager.SetMenuUIVisibility(false);
        uiManager.SetLenIconVisibility(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        uiManager.SetMenuUIVisibility(true);
        uiManager.SetLenIconVisibility(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void GoToOptions()
    {
        uiManager.SetMenuUIVisibility(false);
        uiManager.SetOptionsVisibility(true);
    }
    public void GoBackToMenu()
    {
        uiManager.SetMenuUIVisibility(true);
        uiManager.SetOptionsVisibility(false);
        uiManager.SetControlsVisibility(false);
    }
    public void GoToControls()
    {
        uiManager.SetControlsVisibility(true);
        uiManager.SetMenuUIVisibility(false);
    }
    public void GoToMenu()
    {
        if (gameSettings != null)
        {
            Destroy(gameSettings.gameObject);
        }
        Time.timeScale = 1f;
        uiManager.LoadNextScene();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
