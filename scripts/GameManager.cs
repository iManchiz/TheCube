using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private List<string> sceneNames = new List<string>(); // Lista de nombres de escenas disponibles
    private int sceneChangeCounter = 0; // Contador de cambios de escena
    public Text counterText; // Referencia al objeto Text en Unity
    public GameObject panel; // Referencia al objeto Panel en Unity
    public float fadeDuration = 3f; // Duración de la transición de opacidad
    public AudioClip panelActivationSound; // Sonido a reproducir al activar el panel
    private AudioSource audioSource; // Referencia al AudioSource para reproducir sonido

    // Lista de GameObjects para mostrar en función del contador
    public List<GameObject> cubes;

    private void Awake()
    {
        panel.SetActive(true);
        StartCoroutine(FadeOutPanel());
        StartCoroutine(DisablePlayerMovementTemporarily());
    }

    private void Start()
    {
        // Suscribirse al evento de carga de escenas
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Obtiene los nombres de todas las escenas disponibles
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            sceneNames.Add(sceneName);
        }

        // Cargar el contador guardado si existe
        sceneChangeCounter = PlayerPrefs.GetInt("sceneChangeCounter", 0);

        // Actualizar el texto en pantalla al inicio
        UpdateCounterText();

        // Obtener referencia al AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Hacer visible el GameObject correspondiente al contador inicial
        UpdateCubeVisibility();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Resetear PlayerPrefs si la escena es "MainMenu"
        if (scene.name == "MainMenu")
        {
            ResetCounter();
            PlayerPrefs.DeleteAll();
        }

        // Hacer visible el panel y comenzar la transición de opacidad
        if (panel != null)
        {
            panel.SetActive(true);
            StartCoroutine(FadeOutPanel());
            StartCoroutine(DisablePlayerMovementTemporarily());
        }
        else
        {
            Debug.LogError("Panel reference not set in GameManager!");
        }

        // Actualizar la visibilidad de los GameObjects según el contador
        UpdateCubeVisibility();
    }

    private IEnumerator FadeOutPanel()
    {
        // Reproducir sonido al activar el panel, si hay un AudioClip definido
        if (panelActivationSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(panelActivationSound);
        }

        // Inicializar variables
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        float elapsedTime = 0f;

        // Mantener el panel visible al 100% de opacidad durante 2 segundos
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1f;
            yield return null;
        }

        // Desvanecer gradualmente el panel hasta que la opacidad sea 0
        while (canvasGroup.alpha > 0)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, (elapsedTime - 2f) / fadeDuration);
            yield return null;
        }

        // Asegurarse de que la opacidad sea exactamente 0 al finalizar
        canvasGroup.alpha = 0f;
    }

    private IEnumerator DisablePlayerMovementTemporarily()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
                yield return new WaitForSeconds(2f);
                playerMovement.enabled = true;
            }
            else
            {
                Debug.LogError("PlayerMovement component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    public void TeleportPlayerToInitialPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(20, 4, 10);
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    public void LoadRandomScene()
    {
        // Comprobar si el contador es igual o superior a 9
        if (sceneChangeCounter >= 9)
        {
            SceneManager.LoadScene("Final");
            return;
        }

        // Generar un número aleatorio para determinar qué escena cargar
        float randomValue = Random.Range(0f, 1f);

        string sceneToLoad;

        if (randomValue < 0.5f) // 50% de probabilidad de cargar la escena "Scene"
        {
            sceneToLoad = "Scene";
        }
        else // 67% de probabilidad de cargar cualquier otra escena
        {
            List<string> otherScenes = new List<string>(sceneNames);
            otherScenes.Remove("Scene");
            otherScenes.Remove("Intro");
            otherScenes.Remove("MainMenu");
            otherScenes.Remove("Final");
            otherScenes.Remove("FinalScene");

            if (otherScenes.Count > 0)
            {
                int randomIndex = Random.Range(0, otherScenes.Count);
                sceneToLoad = otherScenes[randomIndex];
            }
            else
            {
                Debug.LogError("No other scenes available to load.");
                return;
            }
        }

        // Guardar el contador
        PlayerPrefs.SetInt("sceneChangeCounter", sceneChangeCounter);

        // Actualizar el texto en pantalla después de cambiar el contador
        UpdateCounterText();

        // Cargar la escena
        SceneManager.LoadScene(sceneToLoad);
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "Cambios de Escena: " + sceneChangeCounter.ToString();
        }
        else
        {
            Debug.LogError("Counter Text reference not set in GameManager!");
        }
    }

    public void IncrementCounter()
    {
        sceneChangeCounter++;
        PlayerPrefs.SetInt("sceneChangeCounter", sceneChangeCounter);
        UpdateCounterText();
        UpdateCubeVisibility();

        // Comprobar si el contador es igual o superior a 9
        if (sceneChangeCounter >= 9)
        {
            SceneManager.LoadScene("Final");
        }
    }

    public void ResetCounter()
    {
        sceneChangeCounter = 0;
        PlayerPrefs.SetInt("sceneChangeCounter", sceneChangeCounter);
        UpdateCounterText();
        UpdateCubeVisibility();
    }

    private void UpdateCubeVisibility()
    {
        // Hacer que todos los GameObjects en la lista sean invisibles
        foreach (GameObject cube in cubes)
        {
            cube.SetActive(false);
        }

        // Hacer visible el GameObject correspondiente al contador actual
        if (sceneChangeCounter >= 0 && sceneChangeCounter < cubes.Count)
        {
            cubes[sceneChangeCounter].SetActive(true);
        }
    }

    public void HandleCollisionTrue()
    {
        if (SceneManager.GetActiveScene().name == "Scene")
        {
            IncrementCounter();
        }
        else
        {
            ResetCounter();
        }
    }

    public void HandleCollisionFalse()
    {
        if (SceneManager.GetActiveScene().name == "Scene")
        {
            ResetCounter();
        }
        else
        {
            IncrementCounter();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
