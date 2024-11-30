using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;    
    [SerializeField] private GameObject interactable;
    [SerializeField] private ChangeScene door;
    [SerializeField] private TutorialInteract2 Libreta;
    [SerializeField] private GameObject LibretaUI;
    [SerializeField] private GameObject LibretaUI2;


    private float tutorialTime = 0;
    private TutorialInitialDialog TIDialog;
    // Start is called before the first frame update
    void Start()
    {
        
        GameManager.Instance.HideCursor();
        TIDialog = FindObjectOfType<TutorialInitialDialog>();
        Libreta.OnInteractableActivated += HandleLibretaActivated;
    }

    private void Update()
    {
        tutorialTime += Time.deltaTime;
    }

    private void HandleLibretaActivated()
    {
        LibretaUI.gameObject.SetActive(true);
        LibretaUI2.gameObject.SetActive(true);
        TIDialog.SecondDialogue();
        door.SetActiveScript(true);

        SentEvents(tutorialTime);
    }

    public void SentEvents(float tutorialTime)
    {
        TutorialTime btnEvt = new TutorialTime
        {
            tutorial_Time = tutorialTime,
        };

        AnalyticsService.Instance.RecordEvent(btnEvt);
    }
}
