using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
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

    private TutorialInitialDialog TIDialog;

    void Start()
    {
        //GameManager.Instance.HideCursor();
        CursorManager.HideCursor();
        GameManager.Instance.isNoteBookPickedUp = false;
        TIDialog = FindObjectOfType<TutorialInitialDialog>();
        Libreta.OnInteractableActivated += HandleLibretaActivated;
    }

    private void Update()
    {
        NotebookStatusCheck();
    }

    private void NotebookStatusCheck()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LibretaUI.gameObject.SetActive(LibretaUI2.activeSelf);
        }
    }

    private void HandleLibretaActivated()
    {
        Debug.Log("libreta handeada");
        GameManager.Instance.isNoteBookPickedUp = true;
        LibretaUI.gameObject.SetActive(true);
        //LibretaUI2.gameObject.SetActive(true);
        TIDialog.SecondDialogue();
        door.SetActiveScript(true);
    }
}
