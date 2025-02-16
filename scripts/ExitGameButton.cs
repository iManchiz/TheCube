using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        // Si estamos en el editor de Unity, detener la reproducci�n
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una compilaci�n del juego, cerrar la aplicaci�n
        Application.Quit();
#endif
    }
}
