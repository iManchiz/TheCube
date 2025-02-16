using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    // Etiqueta del objeto que representa al jugador
    public string playerTag = "Player";

    // Indica si se debe cargar la siguiente escena en la lista de construcci�n
    public bool loadNextScene = true;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colision� tiene la etiqueta del jugador
        if (other.CompareTag(playerTag))
        {
            // Cargar la siguiente escena si est� habilitado
            if (loadNextScene)
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        // Obtener el �ndice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Comprobar si hay una siguiente escena en la lista de construcci�n
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Cargar la siguiente escena en la lista de construcci�n
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.LogWarning("No hay una escena siguiente en la lista de construcci�n.");
        }
    }
}
