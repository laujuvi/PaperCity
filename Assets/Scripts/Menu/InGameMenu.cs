using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject lenIcon;
    public GameObject options;
    public GameObject controls;
    public Slider mouseSensitivitySlider;

    public delegate void SensitivityChangedHandler();
    public static event SensitivityChangedHandler OnSensitivityChanged;

    [SerializeField] GameSettings gameSettings;
    [SerializeField] SceneLoadManager sceneLoadManager;
    private bool isPaused = false;
    private void Start()
    {
        if(gameSettings != null)
        {
            gameSettings.UpdateSensitivitySlider(mouseSensitivitySlider);

            mouseSensitivitySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        }
        menuUI.SetActive(false);
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
        print("quit");
    }
}
