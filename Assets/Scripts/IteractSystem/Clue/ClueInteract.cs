
using UnityEngine;

public class ClueInteract : BasicClueInteract
{
    [SerializeField] private BasicClueInteract miniClue;
    [SerializeField] private string description;
    private DialogManager dialogManager;
    private GameManager gameManager;

    [Header("ListManager")]
    private ListManager _listManager;

    [Header("Audio Source")]
    [SerializeField] private AudioManager audioManager;

    private BoxMessageManager boxClueMessageManager2;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        gameManager = FindObjectOfType<GameManager>();
        _listManager = FindObjectOfType<ListManager>();
        audioManager = FindObjectOfType<AudioManager>();
        boxClueMessageManager2 = FindObjectOfType<BoxMessageManager>();
    }

    public override void Interact()
    {
        boxClueMessageManager2.SendMessage(base.pickablePJText, Color.white, base.pickableText, Emotions.None);    
        dialogManager.SetEvidenceStatus(gameObject.name, true); // Pongo en true para avisar que el player recogio la evidencia
        _listManager.AddText($"({description})");
        gameManager.CheckCurrentEvidence();
        if(GameManager.Instance.currentEvidence == 0)
        {
            GameManager.Instance.SentFirstClueEvents((int)GameManager.Instance.gameplayTime, base.pickableText);
        }
        //audioManager.PlaySFX(audioManager.clueFound);
        AudioManager.instance.PlaySoundFX(AudioManager.instance.clueFound, transform, 1f);
        print("pick");

        //acá hay un problema, porque si destruimos
        //la evidencia el dialogue manager
        //pierde la referencia
        gameObject.SetActive(false);
        miniClue.gameObject.SetActive(true);
    }
}
