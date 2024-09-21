using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;    
    [SerializeField] private GameObject interactable;
    [SerializeField] private ChangeScene door;
    [SerializeField] private GameObject Libreta;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;   
        StartCoroutine(ChangeText(0, "Finaly back in this old office"));
        StartCoroutine(ChangeText(4, "Press (A-S-D-W) to move"));
        StartCoroutine(ChangeText(8, "Press (Ctrl) to Crouch"));
        StartCoroutine(ChangeText(12, "Press (V) to look up"));
        StartCoroutine(ChangeText(16, "Press (Shift) to Run"));
        StartCoroutine(ChangeText(18, "Press (L) to open Log Menu"));
        StartCoroutine(ChangeText(20, "Press (Left Click) to interact with objects"));
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.activeSelf == false)
        {
            Libreta.SetActive(true);
            StartCoroutine(ChangeText(0, "Press (R) to open the Notebook"));
            StartCoroutine(ChangeText(2, "now let's go solve some cases"));
            door.SetActiveScript(true);
        }
       
    }

    private IEnumerator ChangeText(int time, string text)
    {
        yield return new WaitForSeconds(time);

        tutorialText.text = text;
    }
}
