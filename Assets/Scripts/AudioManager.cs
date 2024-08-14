using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Clips")]
    public AudioClip clueFound;
    public AudioClip clueFound2;
    public AudioClip doorOpening;

    public void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
}
