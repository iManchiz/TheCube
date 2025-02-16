using System.Collections;
using UnityEngine;

public class DelayedAction : MonoBehaviour
{
    // AudioSource para reproducir el sonido
    private AudioSource audioSource;

    // Referencia al GameObject "scream"
    public GameObject scream;

    // Tiempo de espera en segundos
    public float delay = 5f;

    void Start()
    {
        // Obtiene el componente AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();

        // Asegúrate de que el AudioSource no se reproduzca automáticamente
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }

        // Inicia la corrutina
        StartCoroutine(DelayedActions());
    }

    IEnumerator DelayedActions()
    {
        // Espera durante el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Reproduce el sonido
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Hace visible el GameObject "scream"
        if (scream != null)
        {
            scream.SetActive(true);
            // Espera 0.2 segundos antes de hacerlo invisible
            yield return new WaitForSeconds(0.2f);
            scream.SetActive(false);
        }
    }
}
