using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPhoneSound : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip phoneRingSound; // El clip de audio que quieres reproducir.
    [SerializeField] private AudioClip otherButtonsSound; // El clip de audio que quieres reproducir.

    public Button button; // Referencia al bot�n.
    public AudioSource music;
    public List<Button> buttons;

    private void Awake()
    {
        if (button == null)
        {
            Debug.LogWarning("No se encontr� un componente Button en este GameObject. Aseg�rate de agregarlo.");
        }
    }

    private void OnEnable()
    {
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].onClick.AddListener(PlayOtherSounds);
        }
    }

    private void PlaySound()
    {
        if (AudioManager.instance != null)
        {
            if (phoneRingSound != null)
            {
                AudioManager.instance.PlaySoundFX(phoneRingSound, transform, 1f);
                music.Stop();
            }
            else
            {
                Debug.LogWarning("Faltan par�metros para reproducir el sonido. Aseg�rate de asignar un AudioClip y un Transform.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� un AudioManager en la escena. Aseg�rate de que est� configurado correctamente.");
        }
    }

    private void PlayOtherSounds()
    {
        if (AudioManager.instance != null)
        {
            if (otherButtonsSound != null)
            {
                AudioManager.instance.PlaySoundFX(otherButtonsSound, transform, 2f);
            }
        }
    }
}
