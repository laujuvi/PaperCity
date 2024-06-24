using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Text tutorialText;
    [SerializeField] private GameObject interactable;
    [SerializeField] private ChangeScene door;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeText(0, "Finaly back in this old office"));
        StartCoroutine(ChangeText(4, "Press A-S-D-W to move"));
        StartCoroutine(ChangeText(8, "Press Ctrl to Crouch"));
        StartCoroutine(ChangeText(12, "Press V to look up"));
        StartCoroutine(ChangeText(16, "Press Shift to Run"));
        StartCoroutine(ChangeText(20, "Press E to interact with objects"));
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.activeSelf == false)
        {
            StartCoroutine(ChangeText(0, "now let's go solve some cases"));
            door.SetActiveScript(true);
        }
       
    }

    private IEnumerator ChangeText(int time, string text)
    {
        yield return new WaitForSeconds(time);

        tutorialText.text = text;
    }
}
