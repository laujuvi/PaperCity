using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSource; // El componente AudioSource en el AudioManager
    [SerializeField] private AudioClip[] audioClips;  // Lista de AudioClips que se pueden reproducir

    private void Awake()
    {
        // Asegurar que solo haya una instancia de AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�todo para reproducir un audio espec�fico
    public void PlayAudio(AudioClip audioClip)
    {
        AudioClip clip = audioClip;
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + audioClip);
        }
    }

    // M�todo para obtener un AudioClip por su nombre
    private AudioClip GetAudioClipByName(string audioName)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == audioName)
            {
                return clip;
            }
        }
        return null;
    }
}
