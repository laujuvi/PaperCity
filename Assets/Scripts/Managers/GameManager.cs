using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* MANAGERS */
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private DialogManager dialogManager;
    
    /* DELAY */
    [SerializeField] private float _delay;
    public PlayerInteract _playerInteract;
    public PlayerController _playerController;

    /* EVIDENCE MONITORING */
    [SerializeField] private int minEvidence = 1;
    [SerializeField] private int maxEvidence = 2;
    [SerializeField] private GameObject guiltyNPC;
    [SerializeField] private string intermediateMessage;
    [SerializeField] private string definitiveMessage;
    private int currentEvidence = 0;

    private void Start()
    {
        if (_playerInteract != null)
        {
            _playerInteract.OnInteract += DisablePlayerInputs;
        }
        else
        {
            Debug.LogWarning("PlayerInteract component not found in the scene.");
        }
    }
    private void DisablePlayerInputs()
    {
        StartCoroutine(DisablePlayerInputsCoroutine(_delay));
    }
    private IEnumerator DisablePlayerInputsCoroutine(float delay)
    {
        _playerInteract.enabled = false;
        _playerController.enabled = false;
        yield return new WaitForSeconds(delay);
        _playerInteract.enabled = true;
        _playerController.enabled = true;
    }

    public void CheckGuiltyNPC(string NPCName)
    {
        if (currentEvidence < minEvidence) return;
        while (!boxMessageManager.IsDisplayingMessage())
        {
            if (guiltyNPC.name == NPCName)
            {
                Debug.Log("WIN");
                return;
            } else
            {
                Debug.Log("LOSE");
                return;
            }
        }

    }

    public void CheckCurrentEvidence()
    {
        currentEvidence++;
        if (currentEvidence >= maxEvidence) { 
        boxMessageManager.SendMessage("", Color.white, definitiveMessage, Emotions.None);
            return;
        }

        if (currentEvidence >= minEvidence)
        {
            boxMessageManager.SendMessage("", Color.white, intermediateMessage, Emotions.None);
            return;
        }
    }
}
