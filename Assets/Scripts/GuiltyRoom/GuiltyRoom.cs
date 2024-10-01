using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class GuiltyRoom : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private List<NPCInteractable> npcs = new List<NPCInteractable>();
    [SerializeField] private List<Transform> tpPoints = new List<Transform>();
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int minClue = 10;
    [SerializeField] private BoxMessageManager boxMessageManager;
    [SerializeField] private GameObject playerController;
    [SerializeField] private CharacterController characterController;

    private void Start()
    {
        boxMessageManager = FindAnyObjectByType<BoxMessageManager>();
    }
    public void Interact()
    {
        if (GameManager.Instance.currentEvidence >= minClue)
        {
            if (playerController != null)
            {
                characterController.enabled = false;
                playerController.gameObject.transform.position = playerTransform.position;
                characterController.enabled = true; 
            }
            
            for (int i = 0; i < npcs.Count; i++)
            {
                npcs[i].transform.position = tpPoints[i].transform.position;
            }
        }
        else 
        {
            boxMessageManager.SendMessage("Detective", Color.white, "I still need more clues", Emotions.None);
        }
   
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }
}
