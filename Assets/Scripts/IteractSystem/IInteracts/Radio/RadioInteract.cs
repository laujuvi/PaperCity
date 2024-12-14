using System.Collections.Generic;
using UnityEngine;

public class RadioInteract : MonoBehaviour, IInteractable
{
    [SerializeField] public List<AudioClip> radioMusic = new List<AudioClip>();
    [SerializeField] private string interactText;
    private int currentSong = 0;
    private AudioSource audioSource;
    public bool isRadioON = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = radioMusic[0];
        audioSource.Play();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        AudioClip clip = audioClip;
        if (clip != null)
        {
            audioSource.clip = clip;
            Debug.Log(audioSource.clip.name);
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + audioClip);
        }
    }

    public void Interact()
    {
        if (isRadioON)
        {
            print("lol");
            currentSong++;
            if (currentSong <= radioMusic.Count - 1)
            {
                audioSource.clip = radioMusic[currentSong];
                PlayAudio(audioSource.clip);
            }
            else 
            {
                currentSong = 0;
            }
        }
    }

    private void Update()
    {
        if (!isRadioON)
        {
            audioSource.Pause();
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
