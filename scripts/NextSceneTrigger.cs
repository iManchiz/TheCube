using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    // Etiqueta del objeto que representa al jugador
    public string playerTag = "Player";

    // Indica si se debe cargar la siguiente escena en la lista de construcción
    public bool loadNextScene = true;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisionó tiene la etiqueta del jugador
        if (other.CompareTag(playerTag))
        {
            // Cargar la siguiente escena si está habilitado
            if (loadNextScene)
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        // Obtener el índice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Comprobar si hay una siguiente escena en la lista de construcción
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Cargar la siguiente escena en la lista de construcción
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.LogWarning("No hay una escena siguiente en la lista de construcción.");
        }
    }
}
