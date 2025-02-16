using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        // Si estamos en el editor de Unity, detener la reproducción
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una compilación del juego, cerrar la aplicación
        Application.Quit();
#endif
    }
}
