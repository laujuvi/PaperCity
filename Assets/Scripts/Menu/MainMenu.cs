using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string Scenename;
    [SerializeField] private GameObject CreditsPanel;
    [SerializeField] private GameObject ControlPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject BackButton;
    public void LoadScene1()
    {
        SceneManager.LoadScene(Scenename);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        BackButton.SetActive(true);
    }
    public void OpenControls()
    {
        ControlPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        BackButton.SetActive(true);
    }
    public void Close()
    {
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        ControlPanel.SetActive(false);
        BackButton.SetActive(false);
    }
}
