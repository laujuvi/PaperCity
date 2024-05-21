using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cluesText;
    private string defaultCluesText = "Clues found:";
    private int currentEvidence = 0;
    private int totalEvidence = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void UpdateCurrentEvidence(int currentInt)
    {
        currentEvidence = currentInt;
        cluesText.text = $"{defaultCluesText} {currentEvidence}/{totalEvidence}";

    }

    public void UpdateTotalEvidence(int totalInt)
    {
        totalEvidence = totalInt;
        cluesText.text = $"{defaultCluesText} {currentEvidence}/{totalEvidence}";

    }
}
