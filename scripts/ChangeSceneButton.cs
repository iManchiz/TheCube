using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeToNextScene()
    {
        // Obtener el �ndice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Obtener el �ndice de la siguiente escena
        int nextSceneIndex = currentSceneIndex + 1;

        // Verificar si el �ndice de la siguiente escena est� dentro de los l�mites de las escenas en el build
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Cargar la siguiente escena
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No hay m�s escenas en el build.");
        }
    }
}
