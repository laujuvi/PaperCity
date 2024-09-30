using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolumen(float level)
    {
        audioMixer.SetFloat("masterVolume", level);
    }
    public void SetSoundFXVolumen(float level)
    {
        audioMixer.SetFloat("soundFXVolume", level);
    }
    public void SetMusicVolumen(float level)
    {
        audioMixer.SetFloat("musicVolume", level);
    }
}
