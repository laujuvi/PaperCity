using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClueInteract : BasicClueInteract
{
    [SerializeField] private string description;
    [SerializeField] private string clueName;
    private DialogManager dialogManager;
    private GameManager gameManager;

    [Header("ListManager")]
    private ListManager _listManager;

    [Header("Audio Source")]
    [SerializeField] private AudioManager audioManager;

    private BoxMessageManager boxClueMessageManager2;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        gameManager = FindObjectOfType<GameManager>();
        _listManager = FindObjectOfType<ListManager>();
        boxClueMessageManager2 = FindObjectOfType<BoxMessageManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        boxClueMessageManager2.SendMessage(base.pickablePJText, Color.white, base.pickableText, Emotions.None);
        dialogManager.SetEvidenceStatus(gameObject.name, true);
        _listManager.AddText($"({description})");
        gameManager.CheckCurrentEvidence();
        //audioManager.PlaySFX(audioManager.clueFound);
        print("pick");
        //gameObject.SetActive(false);
        Destroy(gameObject);
        
    }
}
