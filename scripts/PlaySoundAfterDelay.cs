using System.Collections; // Asegúrate de tener esta línea
using UnityEngine;

public class PlaySoundAfterDelay : MonoBehaviour
{
    public AudioClip soundClip; // El clip de audio que se reproducirá
    public float delay = 10f; // El tiempo de retraso en segundos
    private AudioSource audioSource;

    void Start()
    {
        // Añadir un componente AudioSource al GameObject si no existe uno
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;

        // Iniciar la rutina para reproducir el sonido después del retraso
        StartCoroutine(PlaySoundWithDelay());
    }

    private IEnumerator PlaySoundWithDelay()
    {
        // Esperar el tiempo especificado en 'delay'
        yield return new WaitForSeconds(delay);

        // Reproducir el sonido
        audioSource.Play();
    }
}
