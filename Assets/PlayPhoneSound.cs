using UnityEngine;
using UnityEngine.UI;

public class PlayPhoneSound : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip soundToPlay; // El clip de audio que quieres reproducir.

    public Button button; // Referencia al bot�n.
    public AudioSource music;

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
    }

    private void PlaySound()
    {
        if (AudioManager.instance != null)
        {
            if (soundToPlay != null)
            {
                AudioManager.instance.PlaySoundFX(soundToPlay, transform, 1f);
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
}
