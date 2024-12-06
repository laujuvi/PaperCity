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
    [SerializeField] SceneLoadManager sceneLoadManager;
    private void Start()
    {
        if(gameSettings != null)
        {
            gameSettings.UpdateSensitivitySlider(mouseSensitivitySlider);

            mouseSensitivitySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        }
        uiManager.SetMenuUIVisibility(false);
        //menuUI.SetActive(false);
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
        //menuUI.SetActive(false);
        uiManager.SetMenuUIVisibility(false);
        uiManager.SetLenIconVisibility(true);
        //lenIcon.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        //menuUI.SetActive(true);
        uiManager.SetMenuUIVisibility(true);
        uiManager.SetLenIconVisibility(false);
        //lenIcon.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void GoToOptions()
    {
        //menuUI.SetActive(false);
        uiManager.SetMenuUIVisibility(false);
        uiManager.SetOptionsVisibility(true);
        //options.SetActive(true);
    }
    public void GoBackToMenu()
    {
        //menuUI.SetActive(true);
        uiManager.SetMenuUIVisibility(true);
        uiManager.SetOptionsVisibility(false);
        //options.SetActive(false);
        uiManager.SetControlsVisibility(false);
        //controls.SetActive(false);
    }
    public void GoToControls()
    {
        //controls.SetActive(true);
        uiManager.SetControlsVisibility(true);
        uiManager.SetMenuUIVisibility(false);
        //menuUI.SetActive(false);
    }
    public void GoToMenu()
    {
        if (gameSettings != null)
        {
            Destroy(gameSettings.gameObject);
        }
        Time.timeScale = 1f;
        sceneLoadManager.LoadNextScene();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
