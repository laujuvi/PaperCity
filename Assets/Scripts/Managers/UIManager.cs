using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Clues UI")]
    [SerializeField] TextMeshProUGUI cluesText;
    private string defaultCluesText = "Clues found:";
    private int currentEvidence = 0;
    private int totalEvidence = 0;

    [Header("Gameobjects UI")]
    [SerializeField] GameObject lenIcon;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject options;
    [SerializeField] GameObject controls;
    public void UpdateCurrentEvidence(int currentInt)
    {
        currentEvidence = currentInt;
        cluesText.text = $"{defaultCluesText} {currentEvidence}";
    }
    public void UpdateTotalEvidence(int totalInt)
    {
        totalEvidence = totalInt;
        cluesText.text = cluesText.text = $"{defaultCluesText} {currentEvidence}";
    }
    public void SetMenuUIVisibility(bool isVisible)
    {
        if (menuUI != null)
        {
            menuUI.SetActive(isVisible);
        }
    }
    public void SetOptionsVisibility(bool isVisible)
    {
        if (options != null)
        {
            options.SetActive(isVisible);
        }
    }
    public void SetControlsVisibility(bool isVisible)
    {
        if (controls != null)
        {
            controls.SetActive(isVisible);
        }
    }
    public void SetLenIconVisibility(bool isVisible)
    {
        if(lenIcon != null)
        {
            lenIcon.SetActive(isVisible);
        }
    }
}