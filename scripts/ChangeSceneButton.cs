using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeToNextScene()
    {
        // Obtener el índice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Obtener el índice de la siguiente escena
        int nextSceneIndex = currentSceneIndex + 1;

        // Verificar si el índice de la siguiente escena está dentro de los límites de las escenas en el build
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Cargar la siguiente escena
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No hay más escenas en el build.");
        }
    }
}
