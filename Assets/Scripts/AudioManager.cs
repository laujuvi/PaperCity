using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSource;

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

    public void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
}
