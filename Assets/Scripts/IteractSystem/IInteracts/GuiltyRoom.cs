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

    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void Interact()
    {
        if (GameManager.Instance.currentEvidence == 7)
        {
            playerController.gameObject.transform.position = playerTransform.position;
        }

        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].transform.position = tpPoints[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
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
