using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource soundFX;

    //[SerializeField] private AudioSource sfxAudioSource;

    [Header("Clips")]
    [Header("Clues")]
    public AudioClip clueFound;
    public AudioClip clueFound2;

    [Header("Doors")]
    public AudioClip doorOpening;
    public AudioClip doorClosing;
    
    [Header("Notebook")]
    public AudioClip notebookCheck;
    public AudioClip notebookClose;
    
    [Header("Defeat / Victory sounds")]
    public AudioClip victorySound;
    public AudioClip defeatSound;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFX(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFX, spawnTransform.position, Quaternion.identity);

        if (!audioSource.enabled)
        {
            audioSource.enabled = true;
        }

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
